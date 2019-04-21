using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using MyBot.BLL.Contracts;
using System.Threading;
using MyBot.BLL.Core;


namespace MyBot.BLL.Core.Commands
{
    public class StartQuizCommand : Command
    {
        public override string Name { get => "/StartQuiz"; set => throw new NotImplementedException(); }
        public override bool Contains(string command)
        {
            return (command == this.Name);
        }

        public override async Task<bool> ExecuteAsync(Message message, IBotService bot)
        {
            var activeQuiz = BotService.ActiveQuiz;


            //await bot.Client.SendTextMessageAsync(message.Chat.Id, "Ну что ронарод погнали!");
            if (activeQuiz.ContainsKey(message.Chat.Id))
            {
                await bot.Client.SendTextMessageAsync(message.Chat.Id, "Уже запущено");
                return true ;
            }
            var quiz = new Quiz(bot, message);
            activeQuiz.Add(message.Chat.Id, quiz);
            return true;
        }
    }
}
