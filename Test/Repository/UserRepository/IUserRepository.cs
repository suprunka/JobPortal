using Repositories;
using Repository.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.UsersRepository;

namespace Repository
{
    public interface IUserRepository: IRepository<Users>
    {
        bool Create(RepositoryUser t);


    }
}
