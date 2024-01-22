using SimpleBookingSystem.Core.Entities;

namespace SimpleBookingSystem.Core.Interfaces.IRepositories
{
    public interface IBaseRepositoryAsync<T, in TId> where T : class, IBaseEntity<TId>
    {
        IQueryable<T> Entities { get; }

        Task<T?> GetByIdAsync(TId id);

        Task<List<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(TId entity);
    }
}
