using System.ComponentModel.DataAnnotations;

namespace RestApi.DTOs
{
    public class ProjectDto
    {
        public int Idproject { get; set; }

        [Required(ErrorMessage = "Morate unijeti naziv projekta")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Morate unijeti opis projekta")]
        public string Description { get; set; } = null!;

        public DateTime PublishDate { get; set; }

        [Required(ErrorMessage = "Morate unijeti opis projekta")]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        /*[Required(ErrorMessage = "Morate unijeti tražene vještine")]
        public IEnumerable<SkillSetDto> SkillSets { get; set; } = new List<SkillSetDto>();

        [Required(ErrorMessage = "Morate unijeti tip projketa")]
        public ProjectTypeDto ProjectType { get; set; } = null!;*/

        [Required(ErrorMessage = "Morate unijeti tip projketa")]
        public int ProjectTypeId { get; set; }
        [Required(ErrorMessage = "Morate unijeti tražene vještine")]
        public IEnumerable<int> SkillSetIds { get; set; } = new List<int>();

    }
}
