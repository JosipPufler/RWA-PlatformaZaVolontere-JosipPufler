using System.ComponentModel.DataAnnotations;

namespace RestApi.DTOs
{
    public class UserDto
    {
        public int Iduser { get; set; }

        [Required(ErrorMessage = "Morate unijeti korisničko ime")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Morate unijeti ime")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Morate unijeti prezime")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Morate unijeti email")]
        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; }

        public DateTime JoinDate { get; set; }

        public RoleDto Role { get; set; }

        [Required(ErrorMessage = "Morate navesti skill setove")]
        public IEnumerable<SkillSetDto> UserSkillSets { get; set; } = new List<SkillSetDto>();
    }
}
