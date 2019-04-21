using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using MyBot.BLL.Contracts;
using MyBot.Entities;
using User = MyBot.Entities.User;

namespace MyBot.BLL.Core
{
    public class Quiz : IAsyncInitialization, IDisposable
    {
        //private bool _disposed = false;

        private readonly TelegramBotClient _client;
        private readonly long _chatId;

        public IQueryService QueryService { get; }
        public IUserService UserService { get; }

        private Query _currentQuery;
        private StringBuilder _hint;
        private List<int> _closedLetters;

        private List<User> _usersList;

        private System.Timers.Timer _quizTimer;

        public Quiz(IBotService bot, Message message)
        {
            _chatId = message.Chat.Id;
            _client = bot.Client;
            QueryService = bot.QueryService;
            UserService = bot.UserService;

            _currentQuery = new Query();
            _currentQuery = NextQuery();

            SetTimer();
            
            Initialization = InitializationAsync();
        }


        private void SetTimer()
        {
            _quizTimer = new System.Timers.Timer(15000);
            _quizTimer.Elapsed += async (sender, e) => await UpdateQuery();
            _quizTimer.AutoReset = true;
            _quizTimer.Enabled = true;
        }

        public Task Initialization { get; private set; }

        private async Task InitializationAsync()
        {
            await ShowQuery();
        }

        private async Task UpdateQuery()
        {
            if (_closedLetters.Count == 1)
            {
                await _client.SendTextMessageAsync(_chatId,
                    "Никто не дал правильного ответа. \n" +
                    "Правильный ответ: " + _currentQuery.Answer);

                _currentQuery = NextQuery();
            }
            else
            {
                var rand = new Random();
                int i = rand.Next(_closedLetters.Count);
                var iol = _closedLetters[i]; //index open letter
                _hint[iol] = _currentQuery.Answer[iol];
                _closedLetters.Remove(iol);
            }
            await ShowQuery();
        }

        private async Task ShowQuery()
        {
            await _client.SendTextMessageAsync(_chatId,
                _currentQuery.Name + " \n" + _hint);
        }


        public async Task CheckAnswer(Message message)
        {
            if (!(message.Text.ToLower().Equals(_currentQuery.Answer)) && message.Text != "/Next")
                return;

            if (message.Text.ToLower().Equals(_currentQuery.Answer))
            {
                await _client.SendTextMessageAsync(_chatId,  
                    "Верно ответил " + message.From.FirstName + " " + message.From.LastName +
                    "и получает " + _closedLetters.Count + " баллов!\n", replyToMessageId: message.MessageId);
                UpdateScore(message);
            }

            _quizTimer.Stop();
            _currentQuery = NextQuery();
            await ShowQuery();
            _quizTimer.Start();
        }

        private Query NextQuery()
        {
            Random rand = new Random();
            var randomId = rand.Next(1,7400);
            var query = QueryService.GetById(randomId);
            //generate hint
            _hint = new StringBuilder(new string('*', query.Answer.Length));
            _closedLetters = new List<int>();

            for(var i = 0; i < _hint.Length; i++)
            {
                _closedLetters.Add(i);
            }
            return query;
        }

        private void UpdateScore(Message message)
        {
            if (_usersList == null)
                _usersList = new List<User>(UserService.GetByChatId(_chatId));

            if (_usersList != null)
            {
                foreach (var u in _usersList)
                {
                    if (u.UserId == message.From.Id)
                    {
                        u.Score = u.Score + _closedLetters.Count;
                        UserService.UpdateUser(u);
                        return;
                    }
                }
            }

            var user = new User()
            {
                UserId = message.From.Id,
                ChatId = message.Chat.Id,
                Name = message.From.Username,
                Score = 1

            };
            UserService.AddUser(user);
            _usersList.Add(user);
        }

        #region Disposed pattern

        private bool _disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this.Dispose();
                }
                this._disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
