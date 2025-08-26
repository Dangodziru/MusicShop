using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Artists.Delete
{
    public class ArtistDeleteRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Введено неверное значение")]
        public int ArtistId { get; set; }
    }
}
