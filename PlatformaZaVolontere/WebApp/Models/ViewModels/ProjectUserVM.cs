using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels
{
    public class ProjectUserVM
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Project title")]
        public string ProjectTitle { get; set; }
        
        [Display(Name = "Project type")]
        public string ProjectType { get; set; }
        
        [Display(Name = "Username")]
        public string Username { get; set; }
    }
}
