using MyBot.BLL.Contracts;
using MyBot.BLL.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using MyBot.Entities;

namespace MyBot.BLL.Core.BotBLL.Commands
{
    class ScoreCommand : Command
    {
        public override string Name { get => "/Score"; set => throw new NotImplementedException(); }

        public override bool Contains(string command)
        {
           return (command == this.Name);
        }

        public async override Task<bool> ExecuteAsync(Message message, IBotService bot)
        {
            var userService = bot.UserService;
            var chatId = message.Chat.Id;
            var scoreTable = "Score table in this chat:\n";
            //List<User> userList = new List<User>();
            var userlist = userService.GetByChatId(chatId);
            foreach (var _user in userlist)
            {
                scoreTable = scoreTable + String.Format("{0} : {1}\n", _user.Name, _user.Score);
            }

            await bot.Client.SendTextMessageAsync(chatId, scoreTable);
            return true;
        }
    }
}
