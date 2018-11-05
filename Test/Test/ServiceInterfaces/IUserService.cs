using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using JobPortal.Model;

namespace ServiceLibrary
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        User CreateUser(User u);

        [OperationContract]
        User FindUser(int id);

        [OperationContract]
        bool DeleteUser(int id);

        [OperationContract]
        bool EditUser(User u);

        [OperationContract]
        IList<User> GetAll();


    }
}
