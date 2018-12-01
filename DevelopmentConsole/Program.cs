using System;
using Microsoft.Extensions.DependencyInjection;
using MyBot.DAL.Contracts;
using MyBot.DAL.EF;
using MyBot.BLL.Contracts;
using MyBot.BLL.Core;
using System.Threading;
using MyBot.Entities;
using System.Text;
using System.Collections.Generic;

namespace DevelopmentConsole
{
    internal partial  class Program
    {
        private static void Main(string[] args)
        {
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=botdb;Trusted_Connection=True;";

            var services = new ServiceCollection()
                .AddTransient<IQueryService, QueryService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<IWorker, Worker>()
                .AddTransient<IMainContext, MainContext>(contextProvider => { return new MainContext(connectionString); });

            var serviceProvider = services.BuildServiceProvider();

            var queryService = serviceProvider.GetService<IQueryService>();
            var userService = serviceProvider.GetService<IUserService>();
            RunCycle(queryService, userService);
        }

        public static void RunCycle(IQueryService queryService, IUserService userService)
        {
            //var qs = services.BuildServiceProvider().GetService<IQueryService>();
            User line = new User { UserId = 8, ChatId = 9, Name = "Laminat", Score = 0 };
            userService.AddUser(line);
            Console.WriteLine("CHANGED:" + line.Id.ToString() + line.ChatId.ToString() + line.UserId.ToString() + line.Name.ToString() + line.Score.ToString());
            //Encoding enc = Encoding.GetEncoding(1251);

            List<User> userlist = new List<User>();
            userlist = userService.GetByChatId(9);

            string[] lines = System.IO.File.ReadAllLines(@"E:\Dropbox\kewlu\MyBot\source\voprosy-dlya-chat-viktoriny.txt", Encoding.Default);
            int a = 0;

            //foreach(var line in userlist)
            //{
            //Console.WriteLine(line.Id.ToString() + line.ChatId.ToString() + line.UserId.ToString() + line.Name.ToString() + line.Score.ToString());
            //a++;
            //string[] preQuery = line.Split('|');
            ////Console.WriteLine(line);
            //Query query = new Query { Id = a, Name = preQuery[0], Answer = preQuery[1] };
            //queryService.AddQuery(query);
            //if (line.UserId == 9)
            //{
            //    line.Score = line.Score + 5;
            //    userService.UpdateUser(line);
            //    Console.WriteLine("CHANGED:" + line.Id.ToString() + line.ChatId.ToString() + line.UserId.ToString() + line.Name.ToString() + line.Score.ToString());
            //}                                

            //}
            Hint hint = new Hint("NINE:");
            hint.UpdateHint();
            Console.ReadLine();
        }
    }
}
