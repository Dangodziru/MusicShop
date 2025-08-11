using System.ComponentModel.DataAnnotations;

namespace MusicShop.API.Features.Playlist.Request
{
    public class PlaylistUpdateRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Введено неверное значение")]
        public long PlaylistId { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длинна должнабыть больше 3х семиволов и меньше 100")]
        public string Name { get; set; }
    }
}
