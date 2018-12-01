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
            var _activeQuiz = BotService.ActiveQuiz;
            var _message = message;

            await bot.Client.SendTextMessageAsync(message.Chat.Id, "Ну что ронарод погнали нахой!");
            if (_activeQuiz.ContainsKey(_message.Chat.Id))
            {
                await bot.Client.SendTextMessageAsync(_message.Chat.Id, "Уже запущено");
                return true ;
            }
            var _quizService = new QuizService(bot, message.Chat.Id);
            BotService.ActiveQuiz.Add(_message.Chat.Id, _quizService);
            Thread th = new Thread(_quizService.Start);
            th.Start();
            return true;
        }
    }
}
