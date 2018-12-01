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
        private UserRepository userRepository;

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

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }



        public void Save()
        {
            db.SaveChanges();
        }
        #region Disposed pattern

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}