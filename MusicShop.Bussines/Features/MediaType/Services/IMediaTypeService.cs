using MusicShop.Bussines.Features.MediaType.Delete;
using MusicShop.Bussines.Features.MediaType.Get;
using MusicShop.Bussines.Features.MediaType.Insert;
using MusicShop.Bussines.Features.MediaType.Search;
using MusicShop.Bussines.Features.MediaType.Update;

namespace MusicShop.Bussines.Features.MediaType.Services
{
    public interface IMediaTypeService
    {
        Task<bool> Delete(MediaTypeDeleteRequest request);
        Task<Domain.Entities.MediaType?> Get(MediaTypeGetRequest request);
        Task<IEnumerable<Domain.Entities.MediaType>> GetAll();
        Task<long?> Insert(MediaTypeInsertRequest request);
        Task<IEnumerable<Domain.Entities.MediaType>> Search(MediaTypeSearchRequestcs request);
        Task<bool> Update(MediaTypeUpdateRequest request);
    }
}