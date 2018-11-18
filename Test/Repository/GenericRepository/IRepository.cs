using Repository.DbConnection;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public interface IRepository<T> where T : class
    {
        T Create(T obj);

        T Get(Expression<Func<T, bool>> predicate);

        bool Update(T obj);

        bool Delete(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll();

        IQueryable<T> List(Expression<Func<T, bool>> predicate);

        AspNetUsers Login(AspNetUsers account);
        
    }
}