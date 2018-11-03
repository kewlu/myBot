using System;
using System.Collections.Generic;
using System.Linq;
using MyBot.DAL.Contracts;
using MyBot.Entities;

namespace MyBot.DAL.EF
{
    public class QueryRepository : IRepository<Query>
    {
        private IMainContext db;

        public QueryRepository(IMainContext context)
        {
            db = context;
        }

        public void Create(Query item)
        {
            db.Queries.Add(item);
        }

        public void Delete(Int64 id)
        {
            Query query = db.Queries.Find(id);
            if (query != null)
                db.Queries.Remove(query);
        }

        public IEnumerable<Query> Find(Func<Query, bool> predicate)
        {
            return db.Queries.Where(predicate).ToList();
        }

        public Query Get(Int64 id)
        {
            return db.Queries.Find(id);
        }

        public void Update(Query item)
        {
            db.Queries.Update(item);
        }
    }
}
