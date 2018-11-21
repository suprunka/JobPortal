using JobPortal.Model;
using WebJobPortal.Models;

namespace AppJobPortal.Mapping
{
    public class ClassMapper : AutoMapper.Profile
    {
        public ClassMapper()
        {

            CreateMap<Offer, ServiceOfferWebModel>().ReverseMap();

        }
    }
}