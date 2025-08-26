using MusicShop.Domain.Entities;

namespace MusicShop.Data.Dapper
{
    public interface ITrackDapperRepository
    {
        Task<bool> Delete(long trackId);
        Task<Track?> Get(long trackId);
        Task<IEnumerable<Track>> GetAll();
        Task<long?> Insert(Track track);
        Task<bool> IsExist(string name);
        Task<IEnumerable<Track>> Search(string nameSearch);
        Task<bool> Update(Track track);
    }
}