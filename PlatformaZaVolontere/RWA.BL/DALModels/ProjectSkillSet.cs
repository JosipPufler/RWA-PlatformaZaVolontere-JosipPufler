using System;
using System.Collections.Generic;

namespace RWA.BL.DALModels;

public partial class ProjectSkillSet
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public int SkillSetId { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual SkillSet SkillSet { get; set; } = null!;
}
