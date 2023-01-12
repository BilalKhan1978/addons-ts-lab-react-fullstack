using System.ComponentModel.DataAnnotations;

namespace addons_ts_lab_puppiesApi.ViewModels
{
    public class AddPuppyRequestDto
    {
        [Required]
        public string? Name { get; set; }
        
        public IFormFile? Image { get; set; }
        [Required]
        public string? Breed { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
