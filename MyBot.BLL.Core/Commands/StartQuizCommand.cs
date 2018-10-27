using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using MyBot.BLL.Contracts;
using System.Threading;


namespace MyBot.BLL.Core.Commands
{
    class StartQuizCommand : Command
    {
        public override string Name { get => "/Count"; set => throw new NotImplementedException(); }
        public override bool Contains(string command)
        {
            return (command == this.Name);
        }

        public override async Task<bool> ExecuteAsync(Message message, TelegramBotClient client)
        {
            var _CurrentQuizedList = BotService.CurrentQuizesList;
            //foreach (var _chatId in _CurrentQuizedList)
            //{
            //    if (message.Chat.Id == _chatId)
            //        return true;
            //}
            await client.SendTextMessageAsync(message.Chat.Id, "Ну что ронарод погнали нахой!");
            //BotService.CurrentQuizesList.Add(new QuizService(client, message.Chat.Id));
            var _quizService = new QuizService(client, message.Chat.Id);
            //BotService.CurrentQuizesList.Add(message.Chat.Id);
            await _quizService.Start();
            await client.SendTextMessageAsync(message.Chat.Id, "test");
            return true;
        }
    }
}
