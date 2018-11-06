// ABOUT REPOSITORY PATTERN: https://deviq.com/repository-pattern/


using AutoMapper;

using Repository.DbConnection;
using System;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;

namespace Repositories
{

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Table<T> _Table;
        private readonly DataContext db;

        public Repository(DataContext dataContext)
        {
            db = dataContext;
            _Table = db.GetTable<T>();
        }


        public virtual bool Create(T obj)
        {
            try
            {
                _Table.InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
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
            return _Table.FirstOrDefault(predicate);
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
