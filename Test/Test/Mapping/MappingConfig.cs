
using AutoMapper;
using ServiceLibrary.Models;
using static Repository.UsersRepository;

namespace AppJobPortal.Mapping
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Users, RepositoryUser>());
        }
    }
}