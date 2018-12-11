using System.ServiceModel;
using JobPortal.Model;

namespace ServiceLibrary
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        bool CreateUser(User u, string loggingId);

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
        User[] ListByGender(JobPortal.Model.Gender gender);

        [OperationContract]
        User[] ListByRegion(Region region);

        [OperationContract]
        bool EditUserEmail(User u);

        [OperationContract]
        bool AddDescription(User u);      
    }
}
