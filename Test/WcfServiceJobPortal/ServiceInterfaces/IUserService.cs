using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceJobPortal.ServiceInterfaces
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
        [OperationContract]
        bool IsActive(string email);
    }
}
