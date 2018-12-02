using AppJobPortal.Models;
using JobPortal.Model;

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