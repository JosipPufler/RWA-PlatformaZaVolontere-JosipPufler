using System;
using System.Collections.Generic;

namespace RWA.BL.DALModels;

public partial class Song
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Year { get; set; }

    public int GenreId { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Genre Genre { get; set; } = null!;
}
