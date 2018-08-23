using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading.Tasks;
using System;

namespace MyBot.Models.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; set; }

        public abstract Task<bool> ExecuteAsync(Telegram.Bot.Types.Message message, TelegramBotClient client);

        public abstract bool Contains(string command);
    }
}
