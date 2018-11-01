// ABOUT REPOSITORY PATTERN: https://deviq.com/repository-pattern/


using Repository.DbConnection;
using System;
using System.Data.Linq;
using System.Linq; 

namespace Repositories
{
    public class Repository<T> : IRepository<T> where T : UserTable
    {
        private  Table<T> _Table;
      

        public Repository(JobPortalDatabaseDataContext dataContext)
        {
            _Table = dataContext.GetTable<T>();
                //http://web.archive.org/web/20150404154203/https://www.remondo.net/repository-pattern-example-csharp/
        }
        public T Create(T obj)
        {
            _Table.InsertOnSubmit(obj);
            return obj;
        }

        public bool Delete(int id)
        {
            if(id > 0)
            {
                _Table.DeleteOnSubmit(Get(id));
                return true;
            }
            return false;
          
        }

        public bool Update(T obj)
        {
            return true;
        }
        public virtual T Get(int id)
        {
            return _Table.FirstOrDefault();
        }

        public virtual IQueryable<T> GetAll()
        {
            return new T[] { }.AsQueryable();
        }

        public virtual IQueryable<T> List(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _Table.Where(predicate);
        }

    }
}
