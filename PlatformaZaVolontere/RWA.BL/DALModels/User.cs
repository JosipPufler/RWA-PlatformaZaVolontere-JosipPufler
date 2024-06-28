using System;
using System.Collections.Generic;

namespace RWA.BL.DALModels;

public partial class User
{
    public int Iduser { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DateTime JoinDate { get; set; }

    public int RoleId { get; set; }

    public virtual ICollection<ProjectUser> ProjectUsers { get; } = new List<ProjectUser>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<UserSkillSet> UserSkillSets { get; } = new List<UserSkillSet>();
}
