using Microsoft.EntityFrameworkCore;
using MusicShop.Data;
using MusicShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.DataEntity
{
    public class ArtistRepository(ChinookContext context) : IArtistRepository
    {
        public Task<bool> ArtistIsExist(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteArtist(int artistId)
        {
            throw new NotImplementedException();
        }

        public Task<Artist?> Get(int artistId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Artist>> GetAll()
        {
            return await context.Artists.ToListAsync();
        }

        public Task<int?> InsertArtist(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Artist>> Search(string nameSearch)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateArtist(int artistId, string name)
        {
            throw new NotImplementedException();
        }
    }
}
