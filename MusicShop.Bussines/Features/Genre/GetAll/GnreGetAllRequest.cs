using Mediator;
using MusicShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Bussines.Features.Genre.GetAll
{
    internal class GnreGetAllRequest : IRequest<IEnumerable<MusicShop.Domain.Entities.Genre>>
    {
    }
}
