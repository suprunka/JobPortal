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
        //private IMapper _mapper;

        public Repository(DataContext dataContext)
        {
            db = dataContext;
            _Table = db.GetTable<T>();
            /*var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<T, RepositoryUser>();
            });
            _mapper = config.CreateMapper();*/

            //http://web.archive.org/web/20150404154203/https://www.remondo.net/repository-pattern-example-csharp/
            //https://www.codeproject.com/Articles/1239785/Implementing-and-Testing-Repository-Pattern-using

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
