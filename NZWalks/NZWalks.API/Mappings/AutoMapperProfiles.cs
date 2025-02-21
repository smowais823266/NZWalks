using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegionDTO, Region>().ReverseMap();
            CreateMap<Region, AddRegionDTO>().ReverseMap();
            CreateMap<UpdateRegionDTO,Region>().ReverseMap();
            CreateMap<Walk,AddWalksDTO>().ReverseMap();
            CreateMap<Walk,WalksDTO>().ReverseMap();
            CreateMap<Difficulty,DifficultyDTO>().ReverseMap();
            CreateMap<UpdateWalkDTO, Walk>().ReverseMap();
        }
    }
}
