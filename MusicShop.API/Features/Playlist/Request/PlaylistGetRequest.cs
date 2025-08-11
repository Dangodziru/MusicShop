using System.ComponentModel.DataAnnotations;

namespace MusicShop.API.Features.Playlist.Request
{
    public class PlaylistGetRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long PlaylistId { get; set; }
    }
}
