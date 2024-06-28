using System.ComponentModel.DataAnnotations;

namespace RestApi.DTOs
{
    public class ProjectSkillSetDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Morate unijeti id za vještinu")]
        public int SkillSetId { get; set; }

        [Required(ErrorMessage = "Morate unijeti projekt id")]
        public int ProjectId { get; set; }

    }
}
