using AppJobPortal.Mapping;
using AppJobPortal.Models;
using AppJobPortal.UserServiceReference;
using AutoMapper;
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
        private IMapper _mapper;

        public UserController(IUserService proxy)//, IMapper mapper)
        {
            _proxy = proxy;
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<UserAppModel, User>();

            });
            _mapper = config.CreateMapper();

        }
        public UserAppModel Get(int Id)
        {
           return _mapper.Map(_proxy.FindUser(Id), new UserAppModel());
        }
        public bool Edit(UserAppModel user)
        {

            return _proxy.EditUser(_mapper.Map(user, new User()));
                }
        public User[] GetAll()
        {
           return _proxy.GetAll();
        }
    }
}
