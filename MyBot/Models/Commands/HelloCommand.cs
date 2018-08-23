﻿using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading.Tasks;
using System;
using MyBot.Models;


namespace MyBot.Models.Commands
{
    public class HelloCommand : Command
    {
        public override string Name { get => "/start"; set => throw new NotImplementedException(); }

        public override bool Contains(string command)
        {
            //Console.WriteLine(this.Name + "==" + command);
            return (command == this.Name);
        }

        public override async Task<bool> ExecuteAsync(Telegram.Bot.Types.Message message, TelegramBotClient client)
        {           
            await client.SendTextMessageAsync(message.Chat.Id, "тут тип гороскоп. чтобы получить гороскоп на сегодня. пиши аля  " + '\u0022' + "/овен" + '\u0022' + "," + '\u0022' + " /телец" + '\u0022' + "ну ты понел");
            return true;
        }
    }
}



