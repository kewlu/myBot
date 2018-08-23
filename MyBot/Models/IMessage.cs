using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace MyBot.Models
{
     public interface IMessage
     {
        Task GetMessage(Update update);     }
}
