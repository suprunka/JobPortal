// ABOUT REPOSITORY PATTERN: https://deviq.com/repository-pattern/


using Repository.DbConnection;
using ServiceLibrary.DbConnection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;

namespace Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private  Table<T> _Table;
      

        public Repository(DatabaseDataContext dataContext )
        {
            _Table = dataContext.GetTable<T>();
                //http://web.archive.org/web/20150404154203/https://www.remondo.net/repository-pattern-example-csharp/
        }
        public T Create(T obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(T obj)
        {
            throw new NotImplementedException();
        }
        public virtual T Get(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }

        public virtual IQueryable<T> List(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>()
                   .Where(predicate)
                   .AsEnumerable();
        }

    }
}
