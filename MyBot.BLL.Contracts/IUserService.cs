using System;
using System.Collections.Generic;
using System.Text;
using MyBot.Entities;


namespace MyBot.BLL.Contracts
{
    public interface IUserService
    {
        bool AddUser(User user);
        User GetById(Int64 Id);
        List<User> GetByUserId(Int64 UserId);
        bool UpdateUser(User user);
        List<User> GetByChatId(Int64 chatId);
        IEnumerable<User> GetAll();
    }
}
