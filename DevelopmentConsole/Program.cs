using System;
using Microsoft.Extensions.DependencyInjection;
using MyBot.DAL.Contracts;
using MyBot.DAL.EF;
using MyBot.BLL.Contracts;
using MyBot.BLL.Core;
using System.Threading;

namespace DevelopmentConsole
{
    internal partial  class Program
    {
        private static void Main(string[] args)
        {
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=botdb;Trusted_Connection=True;";

            var services = new ServiceCollection()
                .AddTransient<IQueryService, QueryService>()
                .AddTransient<IWorker, Worker>()
                .AddTransient<IMainContext, MainContext>(contextProvider => { return new MainContext(connectionString); });

            var serviceProvider = services.BuildServiceProvider();

            var queryService = serviceProvider.GetService<IQueryService>();

            RunCycle(queryService);
        }

        public static void RunCycle(IQueryService queryService)
        {
            for (Int64 i = 0; i < 2; i++) {
                var query = queryService.GetById(i);
                Console.WriteLine(query.Name.ToString());

                    }
            Console.ReadLine();
        }
    }
}
