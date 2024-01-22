namespace SimpleBookingSystem.Core.Entities
{
    public class Resource : BaseEntity<int>
    {
        public required string Name { get; set; }

        public int Quantity { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
