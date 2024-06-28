using System;
using System.Collections.Generic;

namespace RWA.BL.DALModels;

public partial class Genre
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Song> Songs { get; } = new List<Song>();
}
