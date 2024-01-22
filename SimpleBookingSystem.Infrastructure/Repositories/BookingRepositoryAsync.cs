using SimpleBookingSystem.Core.Entities;
using SimpleBookingSystem.Core.Interfaces.IRepositories;
using SimpleBookingSystem.Infrastructure.Data;

namespace SimpleBookingSystem.Infrastructure.Repositories
{
    public class BookingRepositoryAsync : BaseRepositoryAsync<Booking, int>, IBookingRepositoryAsync
    {
        public BookingRepositoryAsync(AppDbContext context) : base(context)
        {
        }
    }
}
