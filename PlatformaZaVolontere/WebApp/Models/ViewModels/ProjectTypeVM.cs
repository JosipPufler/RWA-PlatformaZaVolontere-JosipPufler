using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels
{
    public class ProjectTypeVM
    {
        [Display(Name = "Id")]
        public int IdprojectType { get; set; }

        [Display(Name = "Project type")]
        [Required(ErrorMessage = "A project type has to have a name")]
        public string Name { get; set; } = null!;

    }
}
