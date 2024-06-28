using System.ComponentModel.DataAnnotations;

namespace RestApi.DTOs
{
    public class ProjectUserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Morate unijeti id za projekt")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Morate unijeti korisnikov id")]
        public int UserId { get; set; }

    }
}
