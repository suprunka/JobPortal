using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using JobPortal.Model;
using Repository.DbConnection.Entity;

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
        bool CreateAdminUser(User u);

        [OperationContract]
        bool EditUser(User u);

        [OperationContract]
        User[] GetAll();

        [OperationContract]
        User[] ListByGender(Gender gender);

        [OperationContract]
        User[] ListByRegion(Region region);

        [OperationContract]
        bool EditUserEmail(User u);

        [OperationContract]
        bool Login(string username, string password);

        [OperationContract]
        bool AddDescription(User u);

        //[OperationContract]
        //JobPortalEntities GetLoginEntity();
    }
}
