using System.ComponentModel.DataAnnotations;

namespace MusicShop.API.Features.Artist.Request
{
    public class ArtistGetRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long ArtistId { get; set; }
    }
}
