using Mediator;
using MusicShop.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Albums.Get
{
    public class GetRequest : IRequest<Album?>
    {
        [Range(1, long.MaxValue,ErrorMessage = "Введено неверное значение")]
        public long AlbumId { get; set; }
    }
}
