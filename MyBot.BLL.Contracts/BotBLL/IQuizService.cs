using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyBot.BLL.Contracts
{
    public interface IQuizService
    {
        TelegramBotClient _client { get; }

        long _chatId { get; }

        //void Start();

        Task<bool> Stop();

        Task<bool> CheckMessage(Message message);
    }
}
