using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading.Tasks;
using System;
using MyBot.BLL.Contracts;

namespace MyBot.BLL.Core.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; set; }

        public abstract Task<bool> ExecuteAsync(Message message, IBotService bot);

        public abstract bool Contains(string command);
    }
}
