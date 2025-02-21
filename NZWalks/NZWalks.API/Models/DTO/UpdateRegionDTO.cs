using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "The minimum length of Code is 3 characters")]
        [MaxLength(3, ErrorMessage = "The maximum length of Code is 3 characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The length of name should be less then 100 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
