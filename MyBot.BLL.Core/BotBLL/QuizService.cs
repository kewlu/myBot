using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyBot.BLL.Contracts;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading;
using System.ComponentModel;
using MyBot.Entities;

namespace MyBot.BLL.Core
{
    public class QuizService : IQuizService, IDisposable
    {
        public TelegramBotClient _client { get; }
        public IQueryService _queryService { get; }
        public IUserService _userService { get; }
        //private List<Entities.User>  UsersList { get; set; }
        public long _chatId { get; }
        public int a = 0;
        private bool _disposed = false;
        public Query CurrentQuery { get; set; }

        public List<Entities.User> UsersList { get; set; }

        public QuizService(IBotService bot, long chatId)
        {
            _client = bot.Client;
            _chatId = chatId;
            _queryService = bot.QueryService;
            _userService = bot.UserService;
            UsersList = new List<Entities.User>(_userService.GetByChatId(chatId));
        }

        public void Start()
        {
            CurrentQuery = NextQuery();
            for (; ;)
            {
                if (_disposed) return;
                Task<bool> T = Send(CurrentQuery.Name.ToString() + "    \n" + CurrentQuery.Answer.Length + " Букв");
                //_hint.UpdateHint();
                Thread.Sleep(30000);
            }
        }

        public async Task<bool> Send(string a)
        {
            await _client.SendTextMessageAsync(_chatId, a.ToString());
            return true;
        }

        public async Task<bool> Stop()
        {
            _disposed = true;
            await Send("End!");
            BotService.ActiveQuiz.Remove(_chatId);
            //this.Dispose();
            return true;
        }

        public async Task<bool> CheckMessage(Message message)
        {
            if (message.Text == "/Next")
            {
                CurrentQuery = NextQuery();
                return true;
            }
            if(message.Text.ToLower().Equals(CurrentQuery.Answer))
            {
                //await Stop();
                await Send("Верно ответил " + message.From.FirstName + " " + message.From.LastName);
                UpdateScore(message);
                CurrentQuery = NextQuery();
                return true;
            }
            return false;
        }

        public Query NextQuery()
        {
            Random rand = new Random(); //добавь рендж
            var _query = _queryService.GetById(rand.Next(1,7400));
           // Hint _hint = new Hint(_query.Answer);
            return _query;
        }

        private bool UpdateScore(Message message)
        {
            //List<Entities.User> UsersList = new List<Entities.User>(_userService.GetByChatId(message.Chat.Id));
            if (UsersList != null)
            {
                foreach (var user in UsersList)
                {
                    if (user.UserId == message.From.Id)
                    {
                        user.Score = user.Score + 1;
                        _userService.UpdateUser(user);
                        return true;
                    }
                }
            }
           var _user = new Entities.User {UserId = message.From.Id, ChatId = message.Chat.Id, Name = message.From.Username, Score = 1  };
           UsersList.Add(_user);
           _userService.AddUser(_user);
           return true;
        }
        //public

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
