// ABOUT REPOSITORY PATTERN: https://deviq.com/repository-pattern/


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
        private readonly DatabaseDataContext _dbContext;
      

        public Repository(DatabaseDataContext dataContext )
        {
            _dbContext = dataContext;
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

        public virtual IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }

        public virtual IEnumerable<T> List(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>()
                   .Where(predicate)
                   .AsEnumerable();
        }

    }
}
