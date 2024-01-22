using SimpleBookingSystem.Core.Response;
using SimpleBookingSystem.Core.Wrapper;

namespace SimpleBookingSystem.Core.Interfaces.IServices
{
    public interface IResourceService
    {
        Task<IResult<List<ResourceResponse>>> GetAllResroucesAsyn();
    }
}
