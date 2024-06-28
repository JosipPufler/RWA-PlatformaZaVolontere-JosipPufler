namespace RWA.BL.BLModels
{
    public class BlUserSkillSet
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int SkillSetId { get; set; }

        public virtual BlSkillSet SkillSet { get; set; } = null!;

        public virtual BlUser User { get; set; } = null!;
    }
}
