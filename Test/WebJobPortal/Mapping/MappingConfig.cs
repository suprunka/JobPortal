using AutoMapper;
using ServiceLibrary.Models;
using WebJobPortal.Models;


namespace WebJobPortal.Mapping
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserWebModel, Users>();
                cfg.CreateMap<Users, UserWebModel>();
            });

        }
    }
}