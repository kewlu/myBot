using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MyBot.Models
{
    public interface IBot
    {
        TelegramBotClient Client { get; }

        Task InitAsync { get; }
    }
}
