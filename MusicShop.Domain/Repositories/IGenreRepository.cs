using MusicShop.Domain.Entities;

namespace MusicShop.Domain
{
    public interface IGenreRepository
    {
        Task<long?> Insert(string name);
        Task<bool> Delete(long genreId);
        Task<Genre?> Get(long genreId);
        Task<IEnumerable<Genre>> GetAll();
        Task<IEnumerable<Genre>> Search(string term);
        Task<bool> Update(long gereId, string name);
    }
}