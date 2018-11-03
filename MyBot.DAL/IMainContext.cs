using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyBot.Entities;

namespace MyBot.DAL.Contracts
{
    public interface IMainContext : IDisposable
    {
        DbSet<Query> Queries { get; set; }
        DbSet<User> Users { get; set; }

        void SaveChanges();
    }
}
