using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels
{
    public class SkillSetVM
    {
        [Display(Name = "Id")]
        public int IdskillSet { get; set; }

        [Display(Name = "Skill set")]
        [Required(ErrorMessage = "A skill set has to have a name")]
        public string? Name { get; set; }
    }
}
