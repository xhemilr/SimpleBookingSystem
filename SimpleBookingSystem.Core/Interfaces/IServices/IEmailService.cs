namespace SimpleBookingSystem.Core.Interfaces.IServices
{
    public interface IEmailService
    {
        Task SendEmail(int bookingId);
    }
}
