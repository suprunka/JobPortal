using ServiceLibrary.Models;
using static Repository.UsersRepository;

namespace AppJobPortal.Mapping
{
    public class ClassMapper : AutoMapper.Profile
    {
        public ClassMapper()
        {

            CreateMap<Users, RepositoryUser>().ReverseMap();
        }
    }
}