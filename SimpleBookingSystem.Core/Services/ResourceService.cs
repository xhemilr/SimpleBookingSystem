using AutoMapper;
using SimpleBookingSystem.Core.Interfaces.IRepositories;
using SimpleBookingSystem.Core.Interfaces.IServices;
using SimpleBookingSystem.Core.Response;
using SimpleBookingSystem.Core.Wrapper;

namespace SimpleBookingSystem.Core.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepositoryAsync _resourceRepositoryAsync;
        private readonly IMapper _mapper;

        public ResourceService(IResourceRepositoryAsync resourceRepositoryAsync, IMapper mapper)
        {
            _resourceRepositoryAsync = resourceRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<IResult<List<ResourceResponse>>> GetAllResroucesAsyn()
        {
            var resources = await _resourceRepositoryAsync.GetAllAsync();
            var result = _mapper.Map<List<ResourceResponse>>(resources);
            return await Result<List<ResourceResponse>>.SuccessAsync(result);
        }
    }
}
