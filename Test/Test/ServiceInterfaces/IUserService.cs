﻿using ModelClasses;
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
        User CreateUser(User u);
    }  
}
