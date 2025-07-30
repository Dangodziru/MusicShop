using System.ComponentModel.DataAnnotations;

namespace MusicShop.API.Features.Albums.Requests
{
    public class AlbumSearchRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string TitleSearch { get; set; }
    }
}
