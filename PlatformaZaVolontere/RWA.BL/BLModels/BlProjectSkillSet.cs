namespace RWA.BL.BLModels
{
    public class BlProjectSkillSet
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public int SkillSetId { get; set; }

        public virtual BlProject Project { get; set; } = null!;

        public virtual BlSkillSet SkillSet { get; set; } = null!;
    }
}
