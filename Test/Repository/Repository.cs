// ABOUT REPOSITORY PATTERN: https://deviq.com/repository-pattern/


using Repository.DbConnection;
using System;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;

namespace Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly Table<T> _Table;
        protected readonly JobPortalDatabaseDataContext db = null;

        public Repository(JobPortalDatabaseDataContext dataContext)
        {
            db = dataContext;
            _Table = db.GetTable<T>();

            //http://web.archive.org/web/20150404154203/https://www.remondo.net/repository-pattern-example-csharp/
            //https://www.codeproject.com/Articles/1239785/Implementing-and-Testing-Repository-Pattern-using
        }
        public virtual T Create(T obj)
        {
            _Table.InsertOnSubmit(obj);
            return obj;
        }

        public virtual bool Delete(Expression<Func<T, bool>> predicate)
        {
            try
            {
                _Table.DeleteOnSubmit(Get(predicate));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool Update(T obj)
        {
            _Table.InsertOnSubmit(obj);
            return true;
        }
        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            T o = _Table.FirstOrDefault(predicate);
            return o;
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> listOfAll = _Table;
            return listOfAll;
        }

        public virtual IQueryable<T> List(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> filteredList = _Table.Where(predicate);
            return filteredList;
        }
    }


}
