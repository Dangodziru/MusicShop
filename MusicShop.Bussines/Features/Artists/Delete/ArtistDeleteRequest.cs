using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Artists.Delete
{
    public class ArtistDeleteRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long ArtistId { get; set; }
    }
}
