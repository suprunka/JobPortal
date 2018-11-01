using AppJobPortal.Models;
using AppJobPortal.UserServiceReference;
using AutoMapper;



namespace AppJobPortal.Mapping
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<UserAppModel, User>());
        }
    }
}