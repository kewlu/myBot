using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace MyBot.BLL.Contracts
{
     public interface IMessageService
     {
        Task GetMessage(Update update);
     }
}
