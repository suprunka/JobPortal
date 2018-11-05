using AppJobPortal.Models;
using AppJobPortal.UserServiceReferenceTcp;

namespace AppJobPortal.Mapping
{
    public class ClassMapper : AutoMapper.Profile
    {
        public ClassMapper()
        {

            CreateMap<UserAppModel, User>().ReverseMap();
        }
    }
}