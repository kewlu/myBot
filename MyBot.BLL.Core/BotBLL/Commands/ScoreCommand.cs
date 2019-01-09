using MyBot.BLL.Contracts;
using MyBot.BLL.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using MyBot.Entities;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using User = Telegram.Bot.Types.User;

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
            var sorteduserlist = userlist.OrderByDescending(u => u.Score);
            foreach (var user in sorteduserlist)
            {
                scoreTable = scoreTable + String.Format("{0} : {1}\n", user.Name, user.Score);
            }

            await bot.Client.SendTextMessageAsync(chatId, scoreTable);
            return true;
        }
    }
}
