using System.ComponentModel.DataAnnotations;

namespace MusicShop.API.Features.Genre.Request
{
    public class GenreSearchRequestcs
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длинна должнабыть больше 3х семиволов и меньше 100")]
        public string Term { get; set; }
    }
}
