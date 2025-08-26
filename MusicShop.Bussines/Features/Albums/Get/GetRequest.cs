using Mediator;
using MusicShop.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MusicShop.Bussines.Features.Albums.Get
{
    public class GetRequest : IRequest<Album?>
    {
        [Range(1, int.MaxValue,ErrorMessage = "Введено неверное значение")]
        public int AlbumId { get; set; }
    }
}
