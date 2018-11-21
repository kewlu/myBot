using System;
using Microsoft.Extensions.DependencyInjection;
using MyBot.DAL.Contracts;
using MyBot.DAL.EF;
using MyBot.BLL.Contracts;
using MyBot.BLL.Core;
using System.Threading;
using MyBot.Entities;

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
            User user1 = new User { Id = 7, UserId = 8, ChatId = 9, Name = "Laminat", Score = 0 };
            userService.AddUser(user1);
            for (Int64 i = 1; i < 4; i++) {
                var query = queryService.GetById(i);
                Console.WriteLine(query.Name.ToString());

                    }
            Console.ReadLine();
        }
    }
}
