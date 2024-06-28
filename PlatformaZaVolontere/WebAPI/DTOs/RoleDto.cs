using System.ComponentModel.DataAnnotations;

namespace RestApi.DTOs
{
    public class RoleDto
    {
        public int Idrole { get; set; }

        [Required(ErrorMessage = "Morate unijeti naziv za ulogu")]
        public string Name { get; set; } = null!;
    }
}
