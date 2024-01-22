using Microsoft.AspNetCore.Mvc;
using SimpleBookingSystem.Core.Interfaces.IServices;
using SimpleBookingSystem.Core.Response;

namespace SimpleBookingSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _resourceService;

        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpGet(nameof(GetAllResources))]
        public async Task<IActionResult> GetAllResources()
        {
            var response = await _resourceService.GetAllResroucesAsyn();
            return Ok(response);
        }
    }
}
