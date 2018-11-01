using AutoMapper;
using ServiceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebJobPortal.Models;


namespace ServiceLibrary.Mapping
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<UserWebModel, User>());

            Mapper.Initialize(cfg => cfg.CreateMap<User, User>());
        }
    }
}