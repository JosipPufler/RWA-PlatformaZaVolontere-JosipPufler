using System.ComponentModel.DataAnnotations;

namespace RestApi.DTOs
{
    public class ProjectTypeDto
    {
        public int IdprojectType { get; set; }

        [Required(ErrorMessage = "Morate unijeti naziv tipa projekta")]
        public string Name { get; set; } = null!;
    }
}
