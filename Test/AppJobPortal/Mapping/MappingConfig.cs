using AutoMapper;
using JobPortal.Model;

namespace AppJobPortal.Mapping
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.UserAppModel, User>());


        }
    }
}