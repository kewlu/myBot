using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MyBot.DAL.Contracts;
using MyBot.Entities;

namespace MyBot.DAL.EF
{
    public class MainContext : DbContext, IMainContext 
    {
        private string ConnectionString { get; set; }
        
        public DbSet<Query> Queries { get; set; }
        public DbSet<User> Users { get; set; }

        public MainContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        void IMainContext.SaveChanges() => base.SaveChanges();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
