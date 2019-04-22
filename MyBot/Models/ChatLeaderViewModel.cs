using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBot.Models
{
    public class ChatLeaderViewModel
    {
        public long CurrentChatId { get; set; }

        public IEnumerable<UserViewModel> user { get; set; }
    }
}
