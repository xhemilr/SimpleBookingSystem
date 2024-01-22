using SimpleBookingSystem.Core.Interfaces.IServices;

namespace SimpleBookingSystem.Core.Services
{
    public class EmailService : IEmailService
    {
        public Task SendEmail(int bookingId)
        {
            Console.WriteLine($"EMAIL SENT TO admin@admin.com FOR CREATED BOOKING WITH ID {bookingId}");
            return Task.CompletedTask;
        }
    }
}
