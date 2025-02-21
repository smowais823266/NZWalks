using System.Globalization;
using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilter;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
       // [Authorize(Roles ="Reader")] 
        public  async Task<IActionResult> GetAll()
        {
            var regions = new List<Region>();
            var regionsDTO = new List<RegionDTO>();

            logger.LogInformation("Region GetAll() method fired!....");

            regions = await regionRepository.GetAllAsync();

            logger.LogInformation($"GetAllRegion returned data:{JsonSerializer.Serialize(regions)}");
            #region commented
            //foreach (var region in regions)
            //{
            //    regionsDTO.Add(new RegionDTO()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl
            //    });
            //}
            #endregion

            return Ok(mapper.Map<List<RegionDTO>>(regions));
        }

        //Get Region by ID
        [HttpGet]
        ////   [Authorize(Roles = "Reader")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await regionRepository.GetRegionByIDAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            #region mapper commented code
            //var regionsDTO = new RegionDTO()
            //{
            //    Id = region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            #endregion

            return Ok(mapper.Map<RegionDTO>(region));
        }


        [HttpPost]
        //     [ValidateModel]
        //   [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionDTO regionDTO)
        {
            var region = new Region();
            #region commented mapper code
            //{
            //    Code = regionDTO.Code,
            //    Name = regionDTO.Name,
            //    RegionImageUrl = regionDTO.RegionImageUrl
            //};
            #endregion>

            region = mapper.Map<Region>(regionDTO);

            await regionRepository.CreateAcyn(region);

            #region commented mapper code
            //var newRegionDTO = new RegionDTO()
            //{
            //    Id = region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            #endregion
            var newRegionDTO = mapper.Map<RegionDTO>(region);
            //return CreatedAtAction(nameof(GetById), new { id = newRegionDTO.Id }, newRegionDTO);

            return Ok(newRegionDTO);
        }


        //[HttpPost]
        //public IActionResult Create([FromBody] AddRegionDTO model)
        //{
        //    // Set a breakpoint here to step into this method
        //    return Ok(new { Message = "Success" });
        //}



        [HttpPut]
        [Route("{Id:Guid}")]
        //    [ValidateModel]
        //   [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update(Guid Id, [FromBody] UpdateRegionDTO regionDTO)
        {
            #region mapper commented code
            //var region = new Region()
            //{
            //    Name = regionDTO.Name,
            //    Code = regionDTO.Code,
            //    RegionImageUrl = regionDTO.RegionImageUrl
            //};
            #endregion

            var region = mapper.Map<Region>(regionDTO);
            region = await regionRepository.UpdateAsync(Id, region);

            if (region == null)
            {
                return NotFound();
            }

            #region mapper commented code
            //var updateRegionDTO = new UpdateRegionDTO();
            //updateRegionDTO.Name = regionDTO.Name;
            //updateRegionDTO.RegionImageUrl = regionDTO.RegionImageUrl;
            //updateRegionDTO.Code = regionDTO.Code;
            #endregion

            var updateRegionDTO = mapper.Map<RegionDTO>(region);
            return Ok(updateRegionDTO);
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        //   [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var region = await regionRepository.DeleteAsync(Id);
            if (region == null)
            {
                return NotFound();
            }

            #region mapper commented code
            //var regionDTO = new RegionDTO()
            //{
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            #endregion


            return Ok(mapper.Map<RegionDTO>(region));
        }

    }
}
