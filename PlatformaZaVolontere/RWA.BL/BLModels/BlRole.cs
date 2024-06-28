using RWA.BL.DALModels;

namespace RWA.BL.BLModels
{
    public class BlRole
    {
        public int Idrole { get; set; }

        public string? Name { get; set; }

        public virtual ICollection<BlUser> Users { get; } = new List<BlUser>();
    }
}
