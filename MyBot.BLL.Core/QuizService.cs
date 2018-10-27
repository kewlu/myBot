using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyBot.BLL.Contracts;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading;

namespace MyBot.BLL.Core
{
    public class QuizService : IQuizService
    {

        public TelegramBotClient _client { get; }

        public long _chatId { get; }

        public int a = 0;

        public QuizService(TelegramBotClient client, long chatId)
        {
            _client = client;
            _chatId = chatId;
        }



        public async Task Start()
        {
            for(; ; )
            {
                await Task.Delay(5000);
                a++;
                await _client.SendTextMessageAsync(_chatId, a.ToString());
               
            }
        }

        public async Task CheckMessage(Message message)
        {
            if(message.Text == "/StopQuiz")
                await _client.SendTextMessageAsync(_chatId, "конец");
            else
                await _client.SendTextMessageAsync(_chatId, message.Text);
        }
    }
}
