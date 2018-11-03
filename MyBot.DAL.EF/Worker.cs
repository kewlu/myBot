using System;
using System.Collections.Generic;
using System.Text;
using MyBot.DAL.Contracts;
using MyBot.Entities;

namespace MyBot.DAL.EF
{
    public class Worker : IWorker
    {
        private IMainContext db;

        private QueryRepository queryRepository;
        //private UserRepository userRepository;

        public Worker(IMainContext context)
        {
            db = context;
        }

        public IRepository<Query> Queries
        {
            get
            {
                if (queryRepository == null)
                    queryRepository = new QueryRepository(db);
                return queryRepository;
            }
        }

        public IRepository<User> Users => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
