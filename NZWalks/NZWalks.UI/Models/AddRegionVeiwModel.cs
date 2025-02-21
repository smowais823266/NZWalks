using System.ComponentModel.DataAnnotations;

namespace NZWalks.UI.Models
{
    public class AddRegionVeiwModel
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
