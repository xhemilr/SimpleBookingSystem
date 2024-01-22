using AutoMapper;
using SimpleBookingSystem.Core.Entities;
using SimpleBookingSystem.Core.Response;

namespace SimpleBookingSystem.Core.Mappings
{
    public class ResourceProfile : Profile
    {
        public ResourceProfile()
        {
            CreateMap<Resource, ResourceResponse>();
        }
    }
}
