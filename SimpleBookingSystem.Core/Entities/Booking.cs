namespace SimpleBookingSystem.Core.Entities
{
    public class Booking : BaseEntity<int>
    {
        public required DateTime DateFrom { get; set; }

        public required DateTime DateTo { get; set; }

        public required int BookedQuantity { get; set; }

        public required int ResourceId { get; set; }

        public required Resource Resource { get; set; }
    }
}
