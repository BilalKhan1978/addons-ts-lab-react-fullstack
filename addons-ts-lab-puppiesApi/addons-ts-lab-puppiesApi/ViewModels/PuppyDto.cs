using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace addons_ts_lab_puppiesApi.ViewModels
{
    public class PuppyDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Breed { get; set; }
        public string? BirthDate { get; set; }
        public string Image { get; set; }
        public string? ImageName { get; set; }
        public string? ImageSrc { get; set; }
    }
}
