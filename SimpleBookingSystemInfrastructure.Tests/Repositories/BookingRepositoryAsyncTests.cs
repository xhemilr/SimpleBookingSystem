using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using SimpleBookingSystem.Core.Entities;
using SimpleBookingSystem.Core.Interfaces.IRepositories;
using SimpleBookingSystem.Infrastructure.Data;
using SimpleBookingSystem.Infrastructure.Repositories;

namespace SimpleBookingSystemInfrastructure.Tests.Repositories
{
    [TestFixture]
    public class BookingRepositoryAsyncTests
    {
        [Test]
        public async Task Should_Return_False_If_Resource_Doesnt_Exist() 
        {
            var instance = CreateInstance(out var resourceRepositoryAsync);

            resourceRepositoryAsync.GetByIdAsync(Arg.Any<int>()).ReturnsNull();

            var result = await instance.GetByIdAsync(1);

            Assert.That(result, Is.False);
        }

        private BookingRepositoryAsync CreateInstance(out IResourceRepositoryAsync resourceRepositoryAsync)
        {
            var dbContext = new AppDbContext(null);
            resourceRepositoryAsync = Substitute.For<IResourceRepositoryAsync>();

            return new BookingRepositoryAsync(dbContext, resourceRepositoryAsync);
        }
    }
}
