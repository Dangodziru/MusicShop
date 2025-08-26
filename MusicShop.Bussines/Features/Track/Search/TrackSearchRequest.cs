using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Bussines.Features.Track.Search
{
    public class TrackSearchRequest
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина должна быть больше 3-х символов и меньше 100")]
        public string Name { get; set; } = null!;
    }
}
