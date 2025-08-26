using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Genre.Search
{
    public class GenreSearchRequests
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина должнабыть больше 3-х семиволов и меньше 100")]
        public string Term { get; set; }
    }
}
