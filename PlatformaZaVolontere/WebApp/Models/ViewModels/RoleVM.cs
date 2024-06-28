using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels
{
    public class RoleVM
    {
        [Display(Name = "Id")]
        public int Idrole { get; set; }

        [Display(Name = "Role name")]
        [Required(ErrorMessage = "A role has to have a name.")]
        public string? Name { get; set; }
    }
}
