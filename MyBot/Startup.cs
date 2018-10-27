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
            services.AddMvc();

            services.AddScoped<IMessageService, MessageService>();
            services.AddSingleton<IBotService, BotService>();
            services.AddScoped<IQuizService, QuizService>();
            

            services.Configure<BotConfig>(Configuration.GetSection("BotConfiguration"));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
