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
        TelegramBotClient client { get; }

        long chatId { get; }

        //void Start();

        Task<bool> Stop();

        Task<bool> CheckMessage(string str);
    }
}
