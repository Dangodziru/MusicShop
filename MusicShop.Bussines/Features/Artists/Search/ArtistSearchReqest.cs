using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Artists.Search
{
    public class ArtistSearchReqest
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string NameSearch { get; set; }
    }
}
