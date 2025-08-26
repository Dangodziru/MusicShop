namespace MusicShop.Domain.Entities
{
    public class Track  
    {
        public long TrackId { get; set; }
        public string Name { get; set; } = null!;
        public long? AlbumId { get; set; }
        public long MediaTypeId { get; set; }
        public long? GenreId { get; set; }
        public string? Composer { get; set; }
        public long Milliseconds { get; set; }
        public long? Bytes { get; set; }
        public decimal UnitPrice { get; set; }

        public Album? Album { get; set; }
        public MediaType? MediaType { get; set; }
        public Genre? Genre { get; set; }

    }
}