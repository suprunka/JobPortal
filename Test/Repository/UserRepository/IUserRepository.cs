using Repository.DbConnection;

namespace Repository
{
    public interface IUserRepository : IRepository<Users>
    {


        Users UpdateWeb(Users newInformation);
        Users Create(Users obj, string loggingId);
    }
}
