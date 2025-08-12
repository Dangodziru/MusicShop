using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Albums.Search
{
    public class AlbumSearchRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string TitleSearch { get; set; }
    }
}
