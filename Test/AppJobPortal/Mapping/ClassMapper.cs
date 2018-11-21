using AppJobPortal.Models;
using AppJobPortal.UserServiceReferenceTcp;
using JobPortal.Model;

namespace AppJobPortal.Mapping
{
    public class ClassMapper : AutoMapper.Profile
    {
        public ClassMapper()
        {

            CreateMap<UserAppModel, UserServiceReferenceTcp.User>().ReverseMap();


        }
    }
}