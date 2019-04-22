using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBot.BLL.Contracts;
using MyBot.Models;
using MyBot.Entities;


namespace MyBot.Controllers
{
    public class HomeController : Controller
    {
        private IUserService UserService;


        public HomeController(IUserService uservice)
        {
            UserService = uservice;


        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Chats()
        {
            var chats = new List<Chat>();
            var ids = UserService.GetAll().Distinct(new ChatComparer());
            foreach (var elem in ids)
            {
                var users = UserService.GetByChatId(elem.ChatId);
                chats.Add(new Chat{Id = elem.ChatId, NumberUsers = users.Count});
            }
            chats.Sort((e1, e2) => e2.NumberUsers.CompareTo(e1.NumberUsers));
            return View(chats);
        }

        public IActionResult Leaders()
        {
            var ladder = new List<UserViewModel>();
            var users = UserService.GetAll().Distinct(new UserComparer());
            foreach (var usr in users)
            {
                long score = 0;
                var user = UserService.GetByUserId(usr.UserId);
                foreach (var scr in user)
                {
                    score = score + scr.Score;
                }
                ladder.Add(new UserViewModel { Name = usr.Name, Score = score });
            }

            
            ladder.Sort((e1, e2) => e2.Score.CompareTo(e1.Score));

            return View(ladder);

        }

        public IActionResult ChatLeader(long chatid)
        {
            var ladder = new List<UserViewModel>();
            var users = UserService.GetByChatId(chatid);
            users.Sort((e1, e2) => e2.Score.CompareTo(e1.Score));
            return View(users);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
