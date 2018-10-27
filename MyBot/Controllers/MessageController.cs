using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using MyBot.BLL.Contracts;


namespace MyBot.Controllers
{
    [Route("api/update")]
    public class MessageController : Controller
    {
        private readonly IMessageService _message;
        public MessageController(IMessageService message)
        {
            _message = message;
        }


        //POST api/update
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            await _message.GetMessage(update);
            return Ok();
        }
    }
}