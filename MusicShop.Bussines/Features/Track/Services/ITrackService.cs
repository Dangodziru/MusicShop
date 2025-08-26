using MusicShop.Bussines.Features.Track.Delete;
using MusicShop.Bussines.Features.Track.Get;
using MusicShop.Bussines.Features.Track.Insert;
using MusicShop.Bussines.Features.Track.Search;
using MusicShop.Bussines.Features.Track.Update;

namespace MusicShop.Bussines.Features.Tracks.Services
{
    public interface ITrackService
    {
        Task<bool> Delete(TrackDeleteRequest request);
        Task<Domain.Entities.Track?> Get(TrackGetRequest request);
        Task<IEnumerable<Domain.Entities.Track>> GetAll();
        Task<long?> Insert(TrackInsertRequest request);
        Task<IEnumerable<Domain.Entities.Track>> Search(TrackSearchRequest request);
        Task<bool> Update(TrackUpdateRequest request);
    }
}