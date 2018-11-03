using System;
using System.Collections.Generic;
using System.Text;
using MyBot.Entities;

namespace MyBot.DAL.Contracts
{
    public interface IWorker : IDisposable
    {
        IRepository<Query> Queries { get; }
        IRepository<User> Users { get; }

        void Save();
    }
}
