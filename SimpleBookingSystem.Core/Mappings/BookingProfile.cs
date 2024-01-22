using AutoMapper;
using SimpleBookingSystem.Core.Entities;
using SimpleBookingSystem.Core.Requests;

namespace SimpleBookingSystem.Core.Mappings
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<BookingRequest, Booking>().ForMember(dest => dest.BookedQuantity, opt=> opt.MapFrom(src => src.Quantity));
        }
    }
}
