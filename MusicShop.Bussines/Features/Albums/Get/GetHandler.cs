using Mediator;
using MusicShop.Data;
using MusicShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Bussines.Features.Albums.Get
{
    public class GetHandler(IAlbumRepository albumRepository) : IRequestHandler<GetRequest, Album?>
    {
        public ValueTask<Album?> Handle(GetRequest request, CancellationToken cancellationToken)
        {
            var result = albumRepository.Get(request.AlbumId);
            return new ValueTask<Album?>(result);
        }
    }
}
