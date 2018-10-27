using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using MyBot.BLL.Core.Commands;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using MihaZupan;
using System;
using MyBot.BLL.Core.Commands.AstrologyCommand;
using MyBot.BLL.Contracts;
using MyBot.Entities;

namespace MyBot.BLL.Core
{
    public class BotService : IBotService
    {
        private readonly BotConfig _config;

        public BotService(IOptions<BotConfig> config)
        {

            _config = config.Value;
            Client = string.IsNullOrEmpty(_config.Socks5Host)
                ? new TelegramBotClient(_config.BotToken)
                : new TelegramBotClient(_config.BotToken, new HttpToSocks5Proxy(_config.Socks5Host, _config.Socks5Port));
            Console.WriteLine("Client created:{0} \n \t {1} \n \t {2}", _config.BotToken, _config.Socks5Host, _config.Socks5Port);


            //InitAsync = _SetWebhookAsync();
            //Add commands
            commandsList = new List<Command>();
            commandsList.Add(new HelloCommand());
            commandsList.Add(new StartQuizCommand());
            //commandsList.Add(new WoLCommand());
      
            foreach(var _sign in AstroSigns)
            {
                commandsList.Add(item: new AstrologyCommand(_sign));
            }
        }

        private static List<Command> commandsList;
        public static IReadOnlyList<Command> Commands => commandsList.AsReadOnly();

        public static IReadOnlyList<string> AstroSigns
        {
            get => new List<string>() { "овен", "телец", "близнецы", "рак",
                                        "лев", "дева", "весы", "скорпион",
                                   "стрелец", "козерог", "водолей", "рыба"};
        }

        public static List<long> CurrentQuizesList { get; set; }

        public TelegramBotClient Client { get; }

        public Task  InitAsync { get; set; }



        //private async Task<bool> _SetWebhookAsync()
        //{
        //    if (_config.Webhook != null)
        //        await Client.SetWebhookAsync(_config.Webhook);
        //    return true;
        //}

    }
}
