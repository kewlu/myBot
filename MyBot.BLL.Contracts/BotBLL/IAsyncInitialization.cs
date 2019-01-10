using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyBot.BLL.Contracts
{
    public interface IAsyncInitialization
    {
        Task Initialization { get; }
    }
}
