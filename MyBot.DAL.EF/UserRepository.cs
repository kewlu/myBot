using System;
using System.Collections.Generic;
using System.Linq;
using MyBot.DAL.Contracts;
using MyBot.Entities;


namespace MyBot.DAL.EF
{
    public class UserRepository : IRepository<User>
    {
        private IMainContext db;

        public UserRepository(IMainContext context)
        {
            db = context;
        }
        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Delete(long id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public User Get(long id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public void Update(User item)
        {
            db.Users.Update(item);
        }
    }
}
