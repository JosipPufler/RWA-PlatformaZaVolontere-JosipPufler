namespace RWA.BL.BLModels
{
    public class BlUser
    {
        public int Iduser { get; set; }

        public string Username { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; }

        public DateTime JoinDate { get; set; }

        public string Password { get; set; } = null!;

        public BlRole Role { get; set; }

        public IEnumerable<BlSkillSet> UserSkillSets { get; set; } = new List<BlSkillSet>();
    }
}