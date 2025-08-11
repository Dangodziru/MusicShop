using MusicShop.Data;
using MusicShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicShop.Tests
{
    internal class FakeArtistRepository : artistRepository
    {
        private List<Artist> artists = new List<Artist>
        {
            new Artist { ArtistId = 1, Name = "Artist1" },
            new Artist { ArtistId = 2, Name = "Artist2" }
        };

        public bool ArtistIsExist(string name)
        {
            return artists.Any(a => a.Name == name);
        }

        public bool DeleteArtist(long artistId)
        {
            var artist = artists.FirstOrDefault(a => a.ArtistId == artistId);
            if (artist != null)
            {
                artists.Remove(artist);
                return true;
            }
            return false;
        }

        public Artist? Get(long artistId)
        {
            return artists.FirstOrDefault(a => a.ArtistId == artistId);
        }

        public List<Artist> GetAll()
        {
            return artists;
        }

        public long? InsertArtist(string name)
        {
            if (name == "bad")
            {
                return null;
            }
            long newId = artists.Any() ? artists.Max(a => a.ArtistId) + 1 : 1;
            var newArtist = new Artist { ArtistId = newId, Name = name };
            artists.Add(newArtist);
            return newId;
        }

        public List<Artist> Search(string nameSearch)
        {
            return artists.Where(a => a.Name.Contains(nameSearch)).ToList();
        }

        public bool UpdateArtist(long artistId, string name)
        {
            var artist = artists.FirstOrDefault(a => a.ArtistId == artistId);
            if (artist != null)
            {
                artist.Name = name;
                return true;
            }
            return false;
        }
    }
}