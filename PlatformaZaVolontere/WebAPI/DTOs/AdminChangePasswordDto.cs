using System.ComponentModel.DataAnnotations;

namespace RestApi.DTOs
{
    public class AdminChangePasswordDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "The new password is required")]
        [StringLength(256, MinimumLength = 8, ErrorMessage = "A password should be at least 8 characters long")]
        public string Password { get; set; } = null!;
    }
}
