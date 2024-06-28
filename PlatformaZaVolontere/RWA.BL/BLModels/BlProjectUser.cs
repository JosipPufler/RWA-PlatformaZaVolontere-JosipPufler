namespace RWA.BL.BLModels
{
    public class BlProjectUser
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public int UserId { get; set; }

        public virtual BlProject Project { get; set; } = null!;

        public virtual BlUser User { get; set; } = null!;
    }
}
