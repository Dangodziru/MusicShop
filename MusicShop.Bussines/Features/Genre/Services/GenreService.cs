using MusicShop.Bussines.Features.Genre.Delete;
using MusicShop.Bussines.Features.Genre.Get;
using MusicShop.Bussines.Features.Genre.Insert;
using MusicShop.Bussines.Features.Genre.Search;
using MusicShop.Bussines.Features.Genre.Update;
using MusicShop.Domain;


namespace MusicShop.Bussines.Features.Genre.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        public async Task<IEnumerable<MusicShop.Domain.Entities.Genre>> GetAll()
        {
            return await genreRepository.GetAll();
        }

        public async Task<MusicShop.Domain.Entities.Genre?> Get(GenreGetRequests request)
        {
            if (request.GenreId <= 0)
            {
                return null;
            }
            return await genreRepository.Get(request.GenreId);
        }

        public async Task<IEnumerable<MusicShop.Domain.Entities.Genre>> Search(GenreSearchRequests request)
        {
            if (string.IsNullOrWhiteSpace(request.Term))
            {
                return await genreRepository.GetAll();
            }
            return await genreRepository.Search(request.Term);
        }

        public async Task<long?> Insert(GenreInsertRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return null;
            }
            bool mediaTypeExists = true;
            if (mediaTypeExists)
            {
                return null;
            }
            return await genreRepository.Insert(request.Name);
        }

        public async Task<bool> Update(GenreUpdateRequest request)
        {
            if (request.GenreId <= 0 || string.IsNullOrWhiteSpace(request.Name))
            {
                return false;
            }
            return await genreRepository.Update(request.GenreId, request.Name);
        }

        public async Task<bool> Delete(GenreDeleteRequest request)
        {
            if (request.GenreId <= 0)
            {
                return false;
            }
            return await genreRepository.Delete(request.GenreId);
        }
    }

    
}
