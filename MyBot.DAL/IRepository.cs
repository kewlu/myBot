using System;
using System.Collections.Generic;
using System.Text;

namespace MyBot.DAL.Contracts
{
    public interface IRepository<T> where T : class
    {
        T Get(Int64 id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(Int64 id);
    }
}
