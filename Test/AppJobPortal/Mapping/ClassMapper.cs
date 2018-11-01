using AppJobPortal.Models;
using AppJobPortal.UserServiceReference;

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