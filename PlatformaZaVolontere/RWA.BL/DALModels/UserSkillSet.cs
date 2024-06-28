using System;
using System.Collections.Generic;

namespace RWA.BL.DALModels;

public partial class UserSkillSet
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SkillSetId { get; set; }

    public virtual SkillSet SkillSet { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
