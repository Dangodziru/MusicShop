//using MusicShop.Data;
//using MusicShop.Domain.Entities;


//namespace MusicShop.Tests
//{
//    internal class FakeArtistRepository : IArtistRepository
//    {
//        private List<Artist> artists = new List<Artist>
//        {
//            new Artist { ArtistId = 1, Name = "Artist1" },
//            new Artist { ArtistId = 2, Name = "Artist2" }
//        };

//        public Task<bool> ArtistIsExist(string name)
//        {
//            return Task.FromResult(artists.Any(a => a.Name == name));
//        }

//        public Task<bool> DeleteArtist(int artistId)
//        {
//            var artist = artists.FirstOrDefault(a => a.ArtistId == artistId);
//            if (artist != null)
//            {

//                artists.Remove(artist);
//                return Task.FromResult(true);
//            }
//            return Task.FromResult(false);
//        }

//        public Task<Artist?> Get(int artistId)
//        {
//            return Task.FromResult(artists.FirstOrDefault(a => a.ArtistId == artistId));
//        }

//        public Task<IEnumerable<Artist>> GetAll()
//        {
//            return Task.FromResult(artists.AsEnumerable());
//        }

//        public Task<int?> InsertArtist(string name)
//        {
//            if (name == "bad")
//            {
//                return Task.FromResult((int?)null);
//            }
//            int newId = artists.Any() ? artists.Max(a => a.ArtistId) + 1 : 1;
//            var newArtist = new Artist { ArtistId = newId, Name = name };
//            artists.Add(newArtist);
//            return Task.FromResult((int?)newId);
//        }

//        public Task<IEnumerable<Artist>> Search(string nameSearch)
//        {
//            return Task.FromResult(artists.Where(a => a.Name.Contains(nameSearch)));
//        }

//        public Task<bool> UpdateArtist(int artistId, string name)
//        {
//            var artist = artists.FirstOrDefault(a => a.ArtistId == artistId);
//            if (artist != null)
//            {
//                artist.Name = name;
//                return Task.FromResult(true);
//            }
//            return Task.FromResult(false);
//        }
//    }
//}