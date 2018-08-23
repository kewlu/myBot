using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace MyBot.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "hi";
        }
    }
}
