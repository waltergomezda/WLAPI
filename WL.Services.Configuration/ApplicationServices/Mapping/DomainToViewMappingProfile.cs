using ApplicationServices.DataTransferObjects.OutputModel;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Mapping
{
    public class DomainToViewMappingProfile : Profile
    {
        public DomainToViewMappingProfile()
        {
            CreateMap<Config, ConfigOutputModel>();
        }
    }
}
