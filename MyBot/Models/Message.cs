using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Net.Http;

namespace MyBot.Models
{
    public class Message : IMessage
    {
        private readonly IBot _bot;



        private readonly ILogger<Message> _logger;
        

        public Message(IBot bot, ILogger<Message> logger)
        {
            _bot = bot;
            _logger = logger;
        }

        public async Task GetMessage(Update update)
        {
            if (update.Type != UpdateType.Message)
            {
                return;
            }
            var message = update.Message;
            _logger.LogInformation("Received Message from {0}: {1}", message.Chat.Id, message.Text);

            var commands = Bot.Commands;
            foreach (var command in commands)
                Console.WriteLine(command);
            foreach (var command in commands)
            {
                Console.WriteLine(command.ToString() + " ? " + message.Text);
                if (command.Contains(message.Text))
                {
                    Console.WriteLine(command.ToString() + "==" + message.Text + "Execute Command");
                    if (!await command.ExecuteAsync(message, _bot.Client))
                        await _bot.Client.SendTextMessageAsync(message.Chat.Id, "упс, что-то пошло не так");
                    break;
                }

            }
            return;

            //if 
        }
    }
}
