using Repository.DbConnection;

namespace Repository
{
    public interface IUserRepository : IRepository<Users>
    {
        Users AddDescription(Users newInformation);
        Users UpdateUserMail(Users newInformation);
    }
}
