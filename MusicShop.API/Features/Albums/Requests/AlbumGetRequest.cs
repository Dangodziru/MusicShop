using System.ComponentModel.DataAnnotations;

namespace MusicShop.API.Features.Albums.Requests
{
    public class AlbumGetRequest
    {
        [Range(1, long.MaxValue,ErrorMessage = "Введено неверное значение")]
        public long AlbumId { get; set; }
    }
}
