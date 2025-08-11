using MusicShop.Domain.Entities;

namespace MusicShop.Domain
{
    public interface IMediaTypeRepository
    {
        Task<bool> DeleteMediaType(long mediaTypeId);
        Task<MediaType?> Get(long mediaTypeId);
        Task<IEnumerable<MediaType>> GetAll();
        Task<long?> InsertMediaType(string name);
        Task<bool> MediaTypeIsExist(string name);
        Task<IEnumerable<MediaType>> Search(string nameSearch);
        Task<bool> UpdateMediaType(long mediaTypeId, string name);
    }
}