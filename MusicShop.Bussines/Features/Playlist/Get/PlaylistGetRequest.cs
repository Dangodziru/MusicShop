using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Playlist.Get
{
    public class PlaylistGetRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long PlaylistId { get; set; }
    }
}
