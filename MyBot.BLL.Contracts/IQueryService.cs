using System;
using System.Collections.Generic;
using System.Text;
using MyBot.Entities;

namespace MyBot.BLL.Contracts
{
    public interface IQueryService
    {
        Query GetById(long bookId);

        bool AddQuery(Query query);
    }
}
