using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MyBot.BLL.Contracts
{
    public interface IBotService
    {
        TelegramBotClient Client { get; }

        Task InitAsync { get; }

        IQueryService QueryService { get; }
        IUserService UserService { get; }

        //Dictionary<Int64, Quiz> ActiveQuiz;
    }
}
