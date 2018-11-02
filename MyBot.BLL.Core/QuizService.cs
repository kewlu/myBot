using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyBot.BLL.Contracts;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading;
using System.ComponentModel;

namespace MyBot.BLL.Core
{
    public class QuizService : IQuizService
    {

        public TelegramBotClient client { get; }

        public long chatId { get; }

        public int a = 0;
        private bool _disposed = false;


        public QuizService(TelegramBotClient _client, long _chatId)
        {
            client = _client;
            chatId = _chatId;
            
        }

        public void Start()
        {
            for (; ; )
            {
                if (_disposed) return;
                a++;
                Task<bool> T = Send(a.ToString());
                Thread.Sleep(5000);
            }
        }

        public async Task<bool> Send(string a)
        {
            await client.SendTextMessageAsync(chatId, a.ToString());
            return true;
        }

        public bool Stop()
        {
            _disposed = true;
            Task<bool> T = Send("End!");
            return true;
        }
    }
}
