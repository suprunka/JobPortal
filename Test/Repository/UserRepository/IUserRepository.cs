using Repository.DbConnection;

namespace Repository
{
    public interface IUserRepository : IRepository<Users>
    {
        Users Update(Users obj, string phoneNumber);

        Users UpdateWeb(Users newInformation);
    }
}
