using System;
using System.Collections.Generic;

namespace RWA.BL.DALModels;

public partial class Role
{
    public int Idrole { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
