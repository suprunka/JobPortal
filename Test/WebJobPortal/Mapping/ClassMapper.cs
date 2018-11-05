using ServiceLibrary.Models;
using WebJobPortal.Models;

namespace WebJobPortal.Mapping
{
    public class ClassMapper : AutoMapper.Profile
    {
        public ClassMapper()
        {

            CreateMap<UserWebModel, Users>().ReverseMap();

            CreateMap<Users, UserWebModel>().ReverseMap();
        }
    }
}