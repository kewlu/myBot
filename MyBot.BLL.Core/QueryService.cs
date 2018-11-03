using System;
using System.Collections.Generic;
using System.Text;
using MyBot.BLL.Contracts;
using MyBot.Entities;
using MyBot.DAL.Contracts;

namespace MyBot.BLL.Core
{
    public class QueryService : IQueryService
    {
        IWorker Database { get; set; }

        public QueryService(IWorker worker)
        {
            Database = worker;
        }

        public Query GetById(Int64 id)
        {
            var query = Database.Queries.Get(id);
            if (query == null) throw new Exception("query with that id doesnt exist");
            return query;
        }
    }
}
