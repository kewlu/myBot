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

        private static TelegramBotClient Client { get; set; }
        private static long ChatId { get; set; }
        public static IQueryService QueryService { get; set; }
        public static IUserService UserService { get; set; }

        private static Query CurrentQuery { get; set; }
        private static StringBuilder Hint { get; set; }
        private static List<int> ClosedLetters;

        private List<User> UsersList { get; set; }

        private static System.Timers.Timer _quizTimer;

        public Quiz(IBotService bot, Message message)
        {
            ChatId = message.Chat.Id;
            Client = bot.Client;
            QueryService = bot.QueryService;
            UserService = bot.UserService;

            CurrentQuery = new Query();
            CurrentQuery = NextQuery();

            SetTimer();
        }

        private static void SetTimer()
        {
            var T = Task.Run(() => ShowQuery());
            _quizTimer = new System.Timers.Timer(15000);
            _quizTimer.Elapsed += async (sender, e) => await UpdateQuery();
            _quizTimer.AutoReset = true;
            _quizTimer.Enabled = true;
        }

        private static async Task UpdateQuery()
        {
            if (ClosedLetters.Count == 1)
            {
                await Client.SendTextMessageAsync(ChatId,
                    "Никто не дал правильного ответа. \n" +
                    "Правильный ответ: " + CurrentQuery.Answer);

                CurrentQuery = NextQuery();
            }
            else
            {
                var rand = new Random();
                int i = rand.Next(ClosedLetters.Count);
                var iol = ClosedLetters[i]; //index open letter
                Hint[iol] = CurrentQuery.Answer[iol];
                ClosedLetters.Remove(iol);
            }
            await ShowQuery();
        }

        private static async Task ShowQuery()
        {
            await Client.SendTextMessageAsync(ChatId,
                CurrentQuery.Name.ToString() + " \n" + Hint);
        }


        public async Task CheckAnswer(Message message)
        {
            if (!(message.Text.ToLower().Equals(CurrentQuery.Answer)) && message.Text != "/Next")
                return;

            if (message.Text.ToLower().Equals(CurrentQuery.Answer))
            {
                await Client.SendTextMessageAsync(ChatId,  
                    "Верно ответил " + message.From.FirstName + " " + message.From.LastName +
                    "и получает " + ClosedLetters.Count + " баллов!\n", replyToMessageId: message.MessageId);
                UpdateScore(message);
            }

            CurrentQuery = NextQuery();

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
            Hint = new StringBuilder(new string('*', query.Answer.Length));
            ClosedLetters = new List<int>();

            for(var i = 0; i < Hint.Length; i++)
            {
                ClosedLetters.Add(i);
            }
            return query;
        }

        private bool UpdateScore(Message message)
        {
            if (UsersList == null)
                UsersList = new List<User>(UserService.GetByChatId(ChatId));

            if (UsersList != null)
            {
                foreach (var u in UsersList)
                {
                    if (u.UserId == message.From.Id)
                    {
                        u.Score = u.Score + ClosedLetters.Count;
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
