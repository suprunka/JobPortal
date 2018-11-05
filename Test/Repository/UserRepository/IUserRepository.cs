using JobPortal.Model;
using Repositories;
using Repository.DbConnection;

namespace Repository
{
    public interface IUserRepository : IRepository<Users>
    {
        bool Create(User t);
        //void Create(User u);
    }
}
