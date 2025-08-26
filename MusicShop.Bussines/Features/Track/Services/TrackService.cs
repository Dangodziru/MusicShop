using MusicShop.Bussines.Features.Track.Delete;
using MusicShop.Bussines.Features.Track.Get;
using MusicShop.Bussines.Features.Track.Insert;
using MusicShop.Bussines.Features.Track.Search;
using MusicShop.Bussines.Features.Track.Update;

using MusicShop.Data.Dapper;

namespace MusicShop.Bussines.Features.Tracks.Services
{
    public class TrackService : ITrackService
    {
        private readonly ITrackDapperRepository trackRepository;

        public TrackService(ITrackDapperRepository trackRepository)
        {
            this.trackRepository = trackRepository;
        }

        public async Task<IEnumerable<MusicShop.Domain.Entities.Track>> GetAll()
        {
            return await trackRepository.GetAll();
        }

        public async Task<MusicShop.Domain.Entities.Track?> Get(TrackGetRequest request)
        {
            if (request.TrackId <= 0)
            {
                return null;
            }
            return await trackRepository.Get(request.TrackId);
        }

        public async Task<IEnumerable<MusicShop.Domain.Entities.Track>> Search(TrackSearchRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return await trackRepository.GetAll();
            }
            return await trackRepository.Search(request.Name);
        }

        public async Task<long?> Insert(TrackInsertRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return null;
            }

            bool trackExists = await trackRepository.IsExist(request.Name);
            if (trackExists)
            {
                return null;
            }

            var track = new MusicShop.Domain.Entities.Track
            {
                Name = request.Name,
                AlbumId = request.AlbumId,
                MediaTypeId = request.MediaTypeId,
                GenreId = request.GenreId,
                Composer = request.Composer,
                Milliseconds = request.Milliseconds,
                Bytes = request.Bytes,
                UnitPrice = request.UnitPrice
            };

            return await trackRepository.Insert(track);
        }

        public async Task<bool> Update(TrackUpdateRequest request)
        {
            if (request.TrackId <= 0 || string.IsNullOrWhiteSpace(request.Name))
            {
                return false;
            }

            var track = new MusicShop.Domain.Entities.Track
            {
                TrackId = request.TrackId,
                Name = request.Name,
                AlbumId = request.AlbumId,
                MediaTypeId = request.MediaTypeId,
                GenreId = request.GenreId,
                Composer = request.Composer,
                Milliseconds = request.Milliseconds,
                Bytes = request.Bytes,
                UnitPrice = request.UnitPrice
            };

            return await trackRepository.Update(track);
        }

        public async Task<bool> Delete(TrackDeleteRequest request)
        {
            if (request.TrackId <= 0)
            {
                return false;
            }
            return await trackRepository.Delete(request.TrackId);
        }
    }
}