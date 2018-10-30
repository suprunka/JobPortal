using System.Linq;

namespace Repositories
{
     public interface IRepository<T> where T : class
    {
        T Create(T obj);
        T Get(int id);
        bool Update(T obj);
        bool Delete(int id);
        IQueryable<T> GetAll();

    }
}