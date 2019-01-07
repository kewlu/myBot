using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using MyBot.BLL.Contracts;
namespace MyBot.BLL.Core.Commands
{
    public class StopQuizCommand : Command
    {
        public override string Name { get => "/StopQuiz" ; set => throw new NotImplementedException(); }

        public override bool Contains(string command)
        {
            return (command == this.Name);
        }

        public override async Task<bool> ExecuteAsync(Message message, IBotService bot)
        {


            if(!BotService.ActiveQuiz.ContainsKey(message.Chat.Id))
            {
                await bot.Client.SendTextMessageAsync(message.Chat.Id, "В этом чате ничего не запущено! Чтобы запустить /Count");
            }
            var quizService = BotService.ActiveQuiz[message.Chat.Id];
            quizService.Dispose();
            return true;

        }
    }
}
