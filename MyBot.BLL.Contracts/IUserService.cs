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
        //User GetByUserId(Int64 UserId);
        
        List<User> GetByChatId(Int64 chatId);
    }
}
