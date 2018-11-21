// ABOUT REPOSITORY PATTERN: https://deviq.com/repository-pattern/


using AutoMapper;

using Repository.DbConnection;
using System;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
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

        public Repository()
        {

        }


        public virtual T Create(T obj)
        {
            try
            {
                _Table.InsertOnSubmit(obj);
                db.SubmitChanges();
                return obj; 
            }
            catch
            {
                return null;
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
            try
            {
                T found = db.GetTable<T>().FirstOrDefault(predicate);
                return found;
            }
            catch
            {
                return null;
            }
        }


        public virtual IQueryable<T> GetAll()
        {
            try
            {
                IQueryable<T> allOffers = db.GetTable<T>();
                return allOffers;
            }
            catch
            {
                return null;
            }
        }


        public virtual IQueryable<T> List(Expression<Func<T, bool>> predicate)
        {
            try
            {
                IQueryable<T> filteredResult = db.GetTable<T>().Where(predicate);
                return filteredResult;
            }
            catch
            {
                return null;
            }
        }

        public virtual AspNetUsers Login(AspNetUsers obj)
        {
            return db.GetTable<AspNetUsers>().Where(x => x.UserName == obj.UserName).FirstOrDefault();
           
        }
    }
}
