using System;
using System.Collections.Generic;

namespace RWA.BL.DALModels;

public partial class SkillSet
{
    public int IdskillSet { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<ProjectSkillSet> ProjectSkillSets { get; } = new List<ProjectSkillSet>();

    public virtual ICollection<UserSkillSet> UserSkillSets { get; } = new List<UserSkillSet>();
}
