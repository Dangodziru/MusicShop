using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Artists.Get
{
    public class ArtistGetRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Введено неверное значение")]
        public int ArtistId { get; set; }
    }
}
