using System;
using System.Collections.Generic;
using System.Text;
using MyBot.BLL.Contracts;
using MyBot.Entities;
using MyBot.DAL.Contracts;
using System.Linq;

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

        public bool AddQuery(Query query)
        {
            //var allQueries = Database.Queries.GetAll();
            //var id = allQueries.Last().Id + 1;
            //query.Id = id;
            Database.Queries.Create(query);
            Database.Save();
            return true;

        }
    }
}
