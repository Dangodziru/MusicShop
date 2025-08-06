using MusicShop.Domain.Entities;

namespace MusicShop.Domain
{
    public interface IGenreRepository
    {
        public long? Insert(Genre genre);
        bool Delete(long genreId);
        Genre? Get(long genreId);
        List<Genre> GetAll();
        List<Genre> Search(string term);
        bool Update(Genre genre);
    }
}