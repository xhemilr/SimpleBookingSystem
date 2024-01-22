using SimpleBookingSystem.Core.Requests;
using SimpleBookingSystem.Core.Wrapper;

namespace SimpleBookingSystem.Core.Interfaces.IServices
{
    public interface IBookingService
    {
        Task<Result> BookResource(BookingRequest request);
    }
}
