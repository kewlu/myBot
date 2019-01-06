using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Telegram.Bot;
using Telegram.Bot.Types;
using MyBot.BLL.Contracts;
using  MyBot.Entities;
using User = MyBot.Entities.User;

namespace MyBot.BLL.Core.BotBLL
{
    public class Quiz : IDisposable
    {
        private bool _disposed = false;

        private static TelegramBotClient Client { get; set; }
        private static long ChatId { get; set; }
        public IQueryService QueryService { get; }
        public  IUserService UserService { get; }

        private static Query CurrentQuery { get; set; }
        private List<Entities.User> UsersList { get; set; }

        private static System.Timers.Timer _quizTimer;

        public Quiz(IBotService bot, Message message)
        {
            ChatId = message.Chat.Id;
            Client = bot.Client;
            QueryService = bot.QueryService;
            UserService = bot.UserService;

            CurrentQuery = NextQuery();
            SetTimer();
        }

        private static void SetTimer()
        {
            _quizTimer.Elapsed += UpdateQuiz;
            _quizTimer.AutoReset = true;
            _quizTimer.Enabled = true;
            _quizTimer = new System.Timers.Timer(30000);
        }

        private static void UpdateQuiz(Object source, ElapsedEventArgs e)
        {
            var T = ShowQuery();
            //if(  )
        }

        private static async Task<bool> ShowQuery()
        {
            await Client.SendTextMessageAsync(ChatId,
                CurrentQuery.Name.ToString() + "    \n" + CurrentQuery.Answer.Length + " Букв");
            return true;
        }
        public async Task<bool> CheckAnswer(Message message)
        {
            if (message.Text == "/Next")
            {
                CurrentQuery = NextQuery();
                _quizTimer.Start();
                return true;
            }

            if (message.Text.ToLower().Equals(CurrentQuery.Answer))
            {
                await Client.SendTextMessageAsync(ChatId, "Верно ответил " + message.From.FirstName + " " + message.From.LastName);
                UpdateScore(message);
                CurrentQuery = NextQuery();
                _quizTimer.Start();
                return true;
            }

            return false;
        }

        private Query NextQuery()
        {
            Random rand = new Random();
            var randomId = rand.Next();
            var query = QueryService.GetById(randomId);
            return query;
        }

        private bool UpdateScore(Message message)
        {
            UsersList = new List<Entities.User>(UserService.GetByChatId(ChatId));
            if (UsersList != null)
            {
                foreach (var u in UsersList)
                {
                    if (u.UserId == message.From.Id)
                    {
                        u.Score = u.Score + 1;
                        UserService.UpdateUser(u);
                        return true;
                    }
                }
            }

            var user = new Entities.User()
            {
                UserId = message.From.Id,
                ChatId = message.Chat.Id,
                Name = message.From.Username,
                Score = 1

            };
            UsersList.Add(user);
            UserService.AddUser(user);
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
