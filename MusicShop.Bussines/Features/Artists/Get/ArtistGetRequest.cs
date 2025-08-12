using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Artists.Get
{
    public class ArtistGetRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long ArtistId { get; set; }
    }
}
