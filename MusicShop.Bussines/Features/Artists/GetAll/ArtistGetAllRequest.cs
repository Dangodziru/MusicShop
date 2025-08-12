using Mediator;
using MusicShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Bussines.Features.Artists.GetAll
{
    internal class ArtistGetAllRequest : IRequest<IEnumerable<Artist>>
    { 

    }
}
