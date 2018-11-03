using System.Threading.Tasks;
using Telegram.Bot;

namespace MyBot.BLL.Contracts
{
    public interface IBotService
    {
        TelegramBotClient Client { get; }

        Task InitAsync { get; }
    }
}
