using System;
using System.Collections.Generic;
using System.Text;

namespace MyBot.Entities
{
    public class User
    {
        public Int64 Id { get; set; }

        public Int64 UserId { get; set; }

        public Int64 ChatId { get; set; }

        public string Name { get; set; }

        public Int64 Score { get; set; }
    }
}
