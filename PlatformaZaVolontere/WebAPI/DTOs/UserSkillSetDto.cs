using System.ComponentModel.DataAnnotations;

namespace RestApi.DTOs
{
    public class UserSkillSetDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Morate unijeti id od korisnika")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Morate unijeti id za vještinu")]
        public int SkillSetId { get; set; }

    }
}
