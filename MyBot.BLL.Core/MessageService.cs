using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Net.Http;
using MyBot.BLL.Contracts;

namespace MyBot.BLL.Core
{
    public class MessageService : IMessageService
    {
        private readonly IBotService _bot;

        private readonly ILogger<MessageService> _logger;        

        public MessageService(IBotService bot, ILogger<MessageService> logger)
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

            var _commands = BotService.Commands;
            //var _currentquizes = BotService.CurrentQuizesList;

            foreach (var command in _commands)
            {
                if (command.Contains(message.Text))
                {
                    Console.WriteLine(command.ToString() + "==" + message.Text + "Execute Command");
                    if (!await command.ExecuteAsync(message, _bot.Client))
                        await _bot.Client.SendTextMessageAsync(message.Chat.Id, "упс, что-то пошло не так");
                    break;
                }
            }
            return;
        }
    }
}
