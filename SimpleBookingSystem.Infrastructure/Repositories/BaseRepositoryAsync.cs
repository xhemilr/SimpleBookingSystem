using Microsoft.EntityFrameworkCore;
using SimpleBookingSystem.Core.Entities;
using SimpleBookingSystem.Core.Interfaces.IRepositories;
using SimpleBookingSystem.Infrastructure.Data;

namespace SimpleBookingSystem.Infrastructure.Repositories
{
    public class BaseRepositoryAsync<T, TId> : IBaseRepositoryAsync<T, TId> where T : BaseEntity<TId>
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _table;

        public BaseRepositoryAsync(AppDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public IQueryable<T> Entities => _table.AsQueryable();

        public async Task<T> AddAsync(T entity)
        {
            await _table.AddAsync(entity);
            await _context.SaveChangesAsync();
            return await Task.FromResult(entity);
        }

        public async Task DeleteAsync(TId id)
        {
            var entityToDelete = await _table.FindAsync(id);
            if(entityToDelete != null)
            {
                _table.Remove(entityToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(TId id)
        {
            return await _table.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            var entityToUpdate = await _table.FindAsync(entity.Id);
            if(entityToUpdate != null)
            {
                _table.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
