using Mediator;
using MusicShop.Data;
using MusicShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Bussines.Features.Albums.GetAll
{
    public class GetAllHandler(IAlbumRepository albumRepository) : IRequestHandler<GetAllRequest, IEnumerable<Album>>
    {
        public ValueTask<IEnumerable<Album>> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            Task<IEnumerable<Album>> result = albumRepository.GetAll();
            return new ValueTask<IEnumerable<Album>>(result);
        }
    }
}
