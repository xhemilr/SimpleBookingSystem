using Microsoft.EntityFrameworkCore;
using SimpleBookingSystem.Core.Entities;
using SimpleBookingSystem.Core.Interfaces.IRepositories;
using SimpleBookingSystem.Infrastructure.Data;

namespace SimpleBookingSystem.Infrastructure.Repositories
{
    public class ResourceRepositoryAsync : BaseRepositoryAsync<Resource, int>, IResourceRepositoryAsync
    {
        public ResourceRepositoryAsync(AppDbContext context) : base(context)
        {
        }
    }
}
