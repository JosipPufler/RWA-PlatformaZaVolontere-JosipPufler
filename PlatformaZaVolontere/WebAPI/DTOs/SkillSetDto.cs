using System.ComponentModel.DataAnnotations;

namespace RestApi.DTOs
{
    public class SkillSetDto
    {
        public int IdskillSet { get; set; }

        [Required(ErrorMessage = "Morate unijeti id za vještinu")]
        public string Name { get; set; } = null!;
    }
}
