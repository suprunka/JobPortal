﻿using AppJobPortal.Models;
using AppJobPortal.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJobPortal.Controllers
{
    public class UserController
    {
        private readonly IUserService _proxy;

        public UserController(IUserService proxy)//, IMapper mapper)
        {
            _proxy = proxy;


        }
        public UserModel Get(int Id)
        {
            return null;
        }
        public UserModel Edit(UserModel user)
        {
            return null;
        }
        public IList<UserModel> GetAll()
        {
            return null;
        }
    }
}
