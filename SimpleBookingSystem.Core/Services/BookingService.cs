using AutoMapper;
using SimpleBookingSystem.Core.Entities;
using SimpleBookingSystem.Core.Interfaces.IRepositories;
using SimpleBookingSystem.Core.Interfaces.IServices;
using SimpleBookingSystem.Core.Requests;
using SimpleBookingSystem.Core.Wrapper;

namespace SimpleBookingSystem.Core.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepositoryAsync _bookingRepositoryAsync;
        private readonly IResourceRepositoryAsync _resourceRepositoryAsync;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public BookingService(
            IBookingRepositoryAsync bookingRepositoryAsync,
            IResourceRepositoryAsync resourceRepositoryAsync,
            IEmailService emailService,
            IMapper mapper)
        {
            _bookingRepositoryAsync = bookingRepositoryAsync;
            _resourceRepositoryAsync = resourceRepositoryAsync;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<Result> BookResource(BookingRequest request)
        {
            if(!request.DateFrom.HasValue || !request.DateTo.HasValue)
                return await Result.FailAsync("Invalid request!");

            if (request.DateFrom.Value.CompareTo(request.DateTo.Value) is 0)
                return await Result.FailAsync("Invalid time period!");

            var isResourceAvailable = await IsResourceAvailable(request.ResourceId, request.DateFrom.Value, request.DateTo.Value, request.Quantity);
            if (isResourceAvailable)
            {
                var booking = await _bookingRepositoryAsync.AddAsync(_mapper.Map<Booking>(request));
                await Task.Run(async () => await _emailService.SendEmail(booking.Id));
                return await Result.SuccessAsync($"Resource booked sucsessfull for period: {request.DateFrom.Value} to {request.DateTo.Value} ");
            }

            return await Result.FailAsync("Resource not available for specified period.");
        }

        private async Task<bool> IsResourceAvailable(int resourceId, DateTime dateFrom, DateTime dateTo, int quantity)
        {
            var resource = await _resourceRepositoryAsync.GetByIdAsync(resourceId);

            if (resource == null) return false;

            var bookedQuantity = _bookingRepositoryAsync.Entities
            .Where(x => x.ResourceId == resourceId)
            .Where(x => (x.DateFrom >= dateFrom && x.DateFrom <= dateTo) || (x.DateTo >= dateFrom && x.DateTo <= dateTo))
            .Sum(x => x.BookedQuantity);


            return (resource.Quantity - bookedQuantity) >= quantity;
        }
    }
}
