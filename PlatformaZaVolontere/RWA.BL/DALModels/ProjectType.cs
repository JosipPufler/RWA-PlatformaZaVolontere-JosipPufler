using System;
using System.Collections.Generic;

namespace RWA.BL.DALModels;

public partial class ProjectType
{
    public int IdprojectType { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; } = new List<Project>();
}
