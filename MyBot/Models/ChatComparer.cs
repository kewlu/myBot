using System;
using System.Collections.Generic;
using System.Text;
using MyBot.Entities;

namespace MyBot.Models
{
    public class ChatComparer : IEqualityComparer<User>
    {
        public bool Equals(User x, User y)
        {

            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.ChatId == y.ChatId;
        }

        public int GetHashCode(User obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;

            int hashProductCode = obj.ChatId.GetHashCode();

            return hashProductCode;
        }
    }
}
