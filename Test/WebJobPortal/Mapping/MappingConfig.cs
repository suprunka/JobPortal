
using AutoMapper;
using JobPortal.Model;
using WebJobPortal.Models;

namespace AppJobPortal.Mapping
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Offer, ServiceOfferWebModel>());
        }
    }
}