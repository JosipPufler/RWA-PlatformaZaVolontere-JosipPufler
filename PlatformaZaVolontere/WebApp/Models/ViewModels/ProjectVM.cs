using RWA.BL.BLModels;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels
{
    public class ProjectVM
    {
        [Display(Name = "Id")]
        public int Idproject { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "A project title is required")]
        public string Title { get; set; } = null!;

        [Display(Name = "Description")]
        [Required(ErrorMessage = "A project description is required")]
        public string Description { get; set; } = null!;

        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }

        [Display(Name = "Start date")]
        [Required(ErrorMessage = "A start date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End date (leave empty if unknown)")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Skill sets")]
        [Required(ErrorMessage = "At least 1 skill set is mandatory")]
        public IEnumerable<int> SkillSets { get; set; } = new List<int>();

        [Display(Name = "Project type")]
        [Required(ErrorMessage = "A project has to have a type")]
        public int ProjectTypeId { get; set; }

        [Display(Name = "Project type")]
        public string ProjectTypeName { get; set; }
    }
}
