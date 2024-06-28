using System;
using System.Collections.Generic;

namespace RWA.BL.DALModels;

public partial class Project
{
    public int Idproject { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime PublishDate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int TypeId { get; set; }

    public virtual ICollection<ProjectSkillSet> ProjectSkillSets { get; } = new List<ProjectSkillSet>();

    public virtual ICollection<ProjectUser> ProjectUsers { get; } = new List<ProjectUser>();

    public virtual ProjectType Type { get; set; } = null!;
}
