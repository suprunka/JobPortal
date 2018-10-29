using ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Repository
{
    public interface IUserRepository
    {
        User CreateUser(User u);
    }
}
