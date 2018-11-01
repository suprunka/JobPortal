using ServiceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebJobPortal.Models;

namespace WebJobPortal.Mapping
{
    public class ClassMapper : AutoMapper.Profile
    {
        public ClassMapper()
        {

            CreateMap<UserWebModel, User>().ReverseMap();
        }
    }
}