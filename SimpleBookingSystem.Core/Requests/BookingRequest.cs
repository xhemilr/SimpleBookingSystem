namespace SimpleBookingSystem.Core.Requests
{
    public struct BookingRequest
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public int Quantity { get; set; }

        public int ResourceId { get; set; }
    }
}
