using System.ComponentModel.DataAnnotations; // using this for [key]
using System.ComponentModel.DataAnnotations.Schema;

namespace addons_ts_lab_puppiesApi.Models
{
    public class Puppy
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? Name { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? Breed { get; set; }
        public DateTime BirthDate { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? ImageName { get; set; }
        
    }
}
