using Microsoft.AspNetCore.Mvc;
using SimpleBookingSystem.Core.Interfaces.IServices;
using SimpleBookingSystem.Core.Requests;
using System.ComponentModel.DataAnnotations;

namespace SimpleBookingSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost(nameof(BookResource))]
        public async Task<IActionResult> BookResource([Required] BookingRequest request)
        {
            var response = await _bookingService.BookResource(request);
            return Ok(response);
        }
    }
}
