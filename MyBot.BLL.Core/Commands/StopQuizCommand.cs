using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyBot.BLL.Core.Commands
{
    public class StopQuizCommand : Command
    {
        public override string Name { get => "/StopQuiz" ; set => throw new NotImplementedException(); }

        public override bool Contains(string command)
        {
            return (command == this.Name);
        }

        public override async Task<bool> ExecuteAsync(Message message, TelegramBotClient client)
        {
            var _message = message;

            if(!BotService.ActiveQuiz.ContainsKey(_message.Chat.Id))
            {
                await client.SendTextMessageAsync(_message.Chat.Id, "В этом чате ничего не запущено! Чтобы запустить /Count");
            }
            var _quizService = BotService.ActiveQuiz[_message.Chat.Id];
            await _quizService.Stop();
            return true;

        }
    }
}
