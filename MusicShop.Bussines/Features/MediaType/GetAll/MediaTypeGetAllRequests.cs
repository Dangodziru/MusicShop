using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediator;

namespace MusicShop.Bussines.Features.MediaType.GetAll
{
    internal class MediaTypeGetAllRequests: IRequest<IEnumerable<MusicShop.Domain.Entities.MediaType>>
    {
    }
}
