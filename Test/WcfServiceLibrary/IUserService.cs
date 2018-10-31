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
        [OperationContract]
        User CreateUser(User u);

        [OperationContract]
        User FindUser(String PhoneNumber);

        [OperationContract]
        bool DeleteUser(String PhoneNumber);

        [OperationContract]
        bool EditUser(User u);
        
    }  
}
