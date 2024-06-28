using System.ComponentModel.DataAnnotations;

namespace RestApi.DTOs
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(256, MinimumLength = 8, ErrorMessage = "Password should be at least 8 characters long")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "FIrst name is required")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Morate unijeti email")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Morate unijeti broj mobitela")]
        public string PhoneNumber { get; set; }
    }
}
