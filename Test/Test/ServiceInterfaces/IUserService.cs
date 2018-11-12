using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        bool EditWebUser(User u);

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
        bool EditWebUserPassword(User u);

        [OperationContract]
        bool EditWebUserEmail(User u);


    }
}
