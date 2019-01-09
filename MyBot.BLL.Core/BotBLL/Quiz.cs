using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Telegram.Bot;
using Telegram.Bot.Types;
using MyBot.BLL.Contracts;
using  MyBot.Entities;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using User = MyBot.Entities.User;

namespace MyBot.BLL.Core
{
    public class Quiz : IDisposable
    {
        //private bool _disposed = false;

        private static TelegramBotClient _client;
        private static long _chatId;
        public static IQueryService QueryService { get; set; }
        public static IUserService UserService { get; set; }

        private static Query _currentQuery;
        private static StringBuilder _hint;
        private static List<int> _closedLetters;

        private List<User> UsersList { get; set; }

        private static System.Timers.Timer _quizTimer;

        public Quiz(IBotService bot, Message message)
        {
            _chatId = message.Chat.Id;
            _client = bot.Client;
            QueryService = bot.QueryService;
            UserService = bot.UserService;

            _currentQuery = new Query();
            _currentQuery = NextQuery();

            SetTimer();
        }

        private static void SetTimer()
        {
            var T = Task.Run(() => ShowQuery().Wait());
            _quizTimer = new System.Timers.Timer(15000);
            _quizTimer.Elapsed += async (sender, e) => await UpdateQuery();
            _quizTimer.AutoReset = true;
            _quizTimer.Enabled = true;
        }

        private static async Task UpdateQuery()
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

        private static async Task ShowQuery()
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

            _currentQuery = NextQuery();

            _quizTimer.Stop();
            await ShowQuery();

            _quizTimer.Start();
        }

        private static Query NextQuery()
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

        private bool UpdateScore(Message message)
        {
            if (UsersList == null)
                UsersList = new List<User>(UserService.GetByChatId(_chatId));

            if (UsersList != null)
            {
                foreach (var u in UsersList)
                {
                    if (u.UserId == message.From.Id)
                    {
                        u.Score = u.Score + _closedLetters.Count;
                        UserService.UpdateUser(u);
                        return true;
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
            UsersList.Add(user);
            return true;
        }

        #region Disposed pattern

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Dispose();
                }
                this.disposed = true;
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
