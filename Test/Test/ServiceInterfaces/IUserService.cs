using ServiceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WCF;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace ServiceLibrary
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        Users CreateUser(Users u);

        [OperationContract]
        Users FindUser(int id);

        [OperationContract]
        bool DeleteUser(int id);

        [OperationContract]
        bool EditUser(Users u);

        [OperationContract]
        IQueryable<Users> GetAll();


    }
}
