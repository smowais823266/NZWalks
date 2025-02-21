using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddWalksDTO
    {
        [Required]
        [MaxLength(100,ErrorMessage ="Max limit of name is 100 characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Max limit of name is 100 characters")]
        public string Description { get; set; }

        [Required]
        [Range(0,50, ErrorMessage ="The range should be between 0 to 50 km")]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
