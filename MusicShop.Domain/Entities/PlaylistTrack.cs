namespace MusicShop.Domain.Entities
{
    public class PlaylistTrack
    {
        public long PlaylistId { get; set; }
        public long TrackId { get; set; }
        public Track Track { get; set; } = null!;
        public Playlist Playlist { get; set; } = null!;
    }
}
