// ABOUT REPOSITORY PATTERN: https://deviq.com/repository-pattern/


using Repository.DbConnection;
using System;
using System.Data.Linq;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using Repository;

namespace Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbSet<T> _Set;
        protected readonly JobPortalDatabaseDataContext db2 = null;
        protected readonly JobPortalEntities db = new JobPortalEntities();

        public Repository(JobPortalDatabaseDataContext dataContext)
        {
            //db = dataContext;

            _Set = db.Set<T>();
            //http://web.archive.org/web/20150404154203/https://www.remondo.net/repository-pattern-example-csharp/
            //https://www.codeproject.com/Articles/1239785/Implementing-and-Testing-Repository-Pattern-using
        }
        public virtual T Create(T obj)
        {
            
            _Set.Add(obj);
            return obj;
        }

        public virtual bool Delete(Expression<Func<T, bool>> predicate)
        {
            try
            {
                _Set.Remove(Get(predicate));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool Update(T obj)
        {
            _Set.Add(obj);
            return true;
        }
        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            T o = _Set.FirstOrDefault(predicate);
            return o;
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> listOfAll = _Set;
            return listOfAll;
        }

        public virtual IQueryable<T> List(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> filteredList = _Set.Where(predicate);
            return filteredList;
        }
    }


}
