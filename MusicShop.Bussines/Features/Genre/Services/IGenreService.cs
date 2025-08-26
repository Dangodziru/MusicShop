using MusicShop.Bussines.Features.Genre.Delete;
using MusicShop.Bussines.Features.Genre.Get;
using MusicShop.Bussines.Features.Genre.Insert;
using MusicShop.Bussines.Features.Genre.Search;
using MusicShop.Bussines.Features.Genre.Update;

namespace MusicShop.Bussines.Features.Genre.Services
{
    public interface IGenreService
    {
        Task<bool> Delete(GenreDeleteRequest request);
        Task<Domain.Entities.Genre?> Get(GenreGetRequests request);
        Task<IEnumerable<Domain.Entities.Genre>> GetAll();
        Task<long?> Insert(GenreInsertRequest request);
        Task<IEnumerable<Domain.Entities.Genre>> Search(GenreSearchRequests request);
        Task<bool> Update(GenreUpdateRequest request);
    }
}