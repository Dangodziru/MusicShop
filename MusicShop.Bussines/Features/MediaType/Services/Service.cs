using MusicShop.Bussines.Features.MediaType.Delete;
using MusicShop.Bussines.Features.MediaType.Get;
using MusicShop.Bussines.Features.MediaType.Insert;
using MusicShop.Bussines.Features.MediaType.Search;
using MusicShop.Bussines.Features.MediaType.Update;
using MusicShop.Domain;
using static MusicShop.Bussines.Features.MediaType.Services.Service;
using MusicShop.Domain.Entities;

namespace MusicShop.Bussines.Features.MediaType.Services
{
    internal class Service
    {
        internal class MediaTypeService : IMediaTypeService
        {
            private readonly IMediaTypeRepository mediaTypeRepository;

            public MediaTypeService(IMediaTypeRepository mediaTypeRepository)
            {
                this.mediaTypeRepository = mediaTypeRepository;
            }

            public async Task<IEnumerable<MusicShop.Domain.Entities.MediaType>> GetAll()
            {
                return await mediaTypeRepository.GetAll();
            }

            public async Task<MusicShop.Domain.Entities.MediaType?> Get(MediaTypeGetRequest request)
            {
                if (request.MediaTypeId <= 0)
                {
                    return null;
                }
                return await mediaTypeRepository.Get(request.MediaTypeId);
            }

            public async Task<IEnumerable<MusicShop.Domain.Entities.MediaType>> Search(MediaTypeSearchRequestcs request)
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return await mediaTypeRepository.GetAll();
                }
                return await mediaTypeRepository.Search(request.Name);
            }

            public async Task<long?> Insert(MediaTypeInsertRequest request)
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return null;
                }
                bool mediaTypeExists = await mediaTypeRepository.MediaTypeIsExist(request.Name);
                if (mediaTypeExists)
                {
                    return null;
                }
                return await mediaTypeRepository.InsertMediaType(request.Name);
            }

            public async Task<bool> Update(MediaTypeUpdateRequest request)
            {
                if (request.MediaTypeId <= 0 || string.IsNullOrWhiteSpace(request.Name))
                {
                    return false;
                }
                return await mediaTypeRepository.UpdateMediaType(request.MediaTypeId, request.Name);
            }

            public async Task<bool> Delete(MediaTypeDeleteRequest request)
            {
                if (request.MediaTypeId <= 0)
                {
                    return false;
                }
                return await mediaTypeRepository.DeleteMediaType(request.MediaTypeId);
            }
        }

    }
}
