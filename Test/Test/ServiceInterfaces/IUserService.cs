using ServiceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceLibrary
{
    [ServiceContract]

    public interface IUserService
    {
        [FaultContract(typeof(User))]
        [OperationContract]
        User CreateUser(User u);

        [OperationContract]
        User FindUser(int id);

        [OperationContract]
        bool DeleteUser(int id);

        [OperationContract]
        bool EditUser(User u);
        [OperationContract]
        IEnumerable<User> GetAll();


    }  
}
