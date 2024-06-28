using RWA.BL.DALModels;
using System.ComponentModel.DataAnnotations;

namespace RWA.BL.BLModels
{
    public class BlProject
    {
        public int Idproject { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime PublishDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public IEnumerable<BlSkillSet> SkillSets { get; set; } = new List<BlSkillSet>();

        public BlProjectType ProjectType { get; set; } = null!;
    }
}
