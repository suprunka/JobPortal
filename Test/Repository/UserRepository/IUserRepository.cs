using Repository.DbConnection;

namespace Repository
{
    public interface IUserRepository : IRepository<Users>
    {

        Users AddDescription(Users newInformation);
        Users CreateAdmin(Users obj);
        Users UpdateUserMail(Users newInformation);
    }
}
