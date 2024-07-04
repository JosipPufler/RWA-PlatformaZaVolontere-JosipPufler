using RWA.BL.BLModels;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels
{
    public class UserVM
    {
        [Display(Name = "Id")]
        public int Iduser { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "A username is required")]
        public string Username { get; set; } = null!;

        [Display(Name = "First name")]
        [Required(ErrorMessage = "A first name is required")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "A last name is required")]
        public string LastName { get; set; } = null!;

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "An e-mail address is required"), EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email { get; set; } = null!;

        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "A phone number is required"), Phone(ErrorMessage = "Invalid phone format")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Join date")]
        public DateTime JoinDate { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [Display(Name = "Role")]
        [Required(ErrorMessage = "A role is required")]
        public int RoleId { get; set; }

        [Display(Name = "Role name")]
        public string? RoleName { get; set; }

        [Display(Name = "Skill sets")]

        public List<int> UserSkillSets { get; set; }
    }
}
