using System;
using System.Collections.Generic;
using System.Linq;
using MyBot.BLL.Contracts;
using MyBot.DAL.Contracts;
using MyBot.Entities;


namespace MyBot.BLL.Core
{
    public class UserService : IUserService
    {
        IWorker Database { get; set; }

        public UserService(IWorker worker)
        {
            Database = worker;
        }

        public bool AddUser(User _user)
        {
            //if (Database.Users.GetAll().Any(x=> x.Id == _user.Id)
            //{
            //    return false;
            //}
            Database.Users.Create(_user);
            Database.Save();
            return true;
        }

        public List<User> GetByChatId(long _chatId)
        {
            return Database.Users.Find(x => x.ChatId == _chatId).ToList();
        }

        public User GetById(long _Id)
        {
            return Database.Users.Get(_Id);
        }

        public bool UpdateUser(User item)
        {
            Database.Users.Update(item);
            Database.Save();
            return true;
        }


        //public User GetByUserId(long UserId)
        //{
        //    throw new NotImplementedException();
        //}

        //public User GetByUserId(long _UserId)
        //{
        //    return Database.Users.Find()
        //}
    }
}
