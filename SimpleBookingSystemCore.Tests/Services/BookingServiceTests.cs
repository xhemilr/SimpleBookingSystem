using AutoMapper;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using SimpleBookingSystem.Core.Entities;
using SimpleBookingSystem.Core.Interfaces.IRepositories;
using SimpleBookingSystem.Core.Interfaces.IServices;
using SimpleBookingSystem.Core.Requests;
using SimpleBookingSystem.Core.Services;

namespace SimpleBookingSystemCore.Tests.Services
{
    [TestFixture]
    public class BookingServiceTests
    {
        [Test]
        public async Task Should_Book_If_Resource_Available()
        {
            var instance = CreateInstance(
                out var _bookingRepositoryAsync, 
                out _,
                out var _emailService);

            var bookingRequest = new BookingRequest { ResourceId = 1, DateFrom = DateTime.Now, DateTo = DateTime.Now.AddDays(1), Quantity = 1 };

            var result = await instance.BookResource(bookingRequest);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Succeeded, Is.True);
            await _bookingRepositoryAsync.Received().AddAsync(Arg.Any<Booking>());
            await _emailService.Received().SendEmail(Arg.Is<int>(x => x == 1));
        }

        [Test]
        public async Task Should_Not_Book_If_Resource_Not_Available()
        {
            var instance = CreateInstance(
                out var _bookingRepositoryAsync,
                out var _resourceRepositoryAsync,
                out var _emailService);

            _resourceRepositoryAsync.GetByIdAsync(Arg.Any<int>()).ReturnsNull();

            var bookingRequest = new BookingRequest { ResourceId = 1, DateFrom = DateTime.Now, DateTo = DateTime.Now.AddDays(1), Quantity = 1 };

            var result = await instance.BookResource(bookingRequest);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Succeeded, Is.False);
            await _bookingRepositoryAsync.DidNotReceive().AddAsync(Arg.Any<Booking>());
            await _emailService.DidNotReceive().SendEmail(Arg.Any<int>());
        }

        [TestCase("01/20/2024", "01/23/2024", "01/20/2024", "01/23/2024", false)]
        [TestCase("01/20/2024", "01/23/2024", "01/22/2024", "01/24/2024", false)]
        [TestCase("01/20/2024", "01/23/2024", "01/20/2024", "01/22/2024", false)]
        [TestCase("01/20/2024", "01/23/2024", "01/24/2024", "01/26/2024", true)]
        [TestCase("01/20/2024", "01/23/2024", "01/14/2024", "01/15/2024", true)]
        public async Task Should_Book_If_Resource_Quantity_Available(
            string bookedDateFrom, 
            string bookedDateTo, 
            string requestedDateFrom, 
            string requestedDateTo,
            bool isBooked
            )
        {
            var instance = CreateInstance(
                out var _bookingRepositoryAsync,
                out _,
                out _);

            var bookedDtFrom = DateTime.Parse(bookedDateFrom);
            var bookedDtTo = DateTime.Parse(bookedDateTo);
            var requestedDtFrom = DateTime.Parse(requestedDateFrom);
            var requestedDtTo = DateTime.Parse(requestedDateTo);

            var bookedResources = new List<Booking>
            {
                new Booking {
                    Id = 1,
                    BookedQuantity = 10,
                    DateFrom = bookedDtFrom,
                    DateTo = bookedDtTo,
                    ResourceId = 1,
                    Resource = new Resource { Id = 1, Name = "Resource1" },
                }
            };

            _bookingRepositoryAsync.Entities.Returns(bookedResources.AsQueryable());

            var bookingRequest = new BookingRequest { ResourceId = 1, DateFrom = requestedDtFrom, DateTo = requestedDtTo, Quantity = 1 };

            var result = await instance.BookResource(bookingRequest);

            Assert.That(result, Is.Not.Null);
            Assert.That(isBooked, Is.EqualTo(result.Succeeded));
        }

        private BookingService CreateInstance(
            out IBookingRepositoryAsync bookingRepositoryAsync,
            out IResourceRepositoryAsync resourceRepositoryAsync,
            out IEmailService emailService)
        {
            bookingRepositoryAsync = Substitute.For<IBookingRepositoryAsync>();
            resourceRepositoryAsync = Substitute.For<IResourceRepositoryAsync>();
            emailService = Substitute.For<IEmailService>();
            var mapper = Substitute.For<IMapper>();

            resourceRepositoryAsync.GetByIdAsync(Arg.Any<int>()).Returns(new Resource { Id = 1, Name = "Resource1", Quantity = 10 });

            bookingRepositoryAsync.AddAsync(Arg.Any<Booking>()).Returns(
                new Booking { 
                    Id = 1,
                    BookedQuantity = 1,
                    DateFrom = DateTime.Now, 
                    DateTo = DateTime.Now.AddDays(1),
                    ResourceId = 1,
                    Resource = new Resource { Id = 1, Name = "Resource1" },
                });

            return new BookingService(bookingRepositoryAsync, resourceRepositoryAsync, emailService, mapper);
        }
    }
}
