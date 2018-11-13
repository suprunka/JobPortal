using System.ServiceModel;
using JobPortal.Model;

namespace ServiceLibrary
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        bool CreateUser(User u);

        [OperationContract]
        User FindUser(string phoneNumber);

        [OperationContract]
        User FindUserByID(int id);

        [OperationContract]
        bool DeleteUser(int id);

        [OperationContract]
        bool EditUser(User u);

        [OperationContract]
        User[] GetAll();

        [OperationContract]
        User[] ListByGender(Gender gender);

        [OperationContract]
        User[] ListByRegion(Region region);

        [OperationContract]
        bool Login(string username, string password);
    }
}
