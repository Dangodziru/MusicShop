using System;
using System.Collections.Generic;

namespace MusicShop.Domain.Entities;

public partial class Playlist
{
    public long PlaylistId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
