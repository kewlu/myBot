using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBot.BLL.Contracts;
using MyBot.BLL.Core;
using MyBot.Entities;
using MyBot.DAL.Contracts;
using MyBot.DAL.EF;


namespace MyBot.PL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=botdb;Trusted_Connection=True;";
            services.AddMvc();

            services.AddScoped<IMessageService, MessageService>();
            services.AddSingleton<IBotService, BotService>();
            services.AddTransient<IQueryService, QueryService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMainContext, MainContext>(contextProvider => { return new MainContext(connectionString); });
            services.AddTransient<IWorker, Worker>();


            services.Configure<BotConfig>(Configuration.GetSection("BotConfiguration"));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
