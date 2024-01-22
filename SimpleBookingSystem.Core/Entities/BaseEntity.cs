namespace SimpleBookingSystem.Core.Entities
{
    public class BaseEntity<TId> : IBaseEntity<TId>
    {
        public required TId Id { get; set; }
    }

    public interface IBaseEntity<TId>
    {
        public TId Id { get; set; }
    }
}
