using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalksRepositorycs walksRepositorycs;
        private readonly IMapper mapper;

        public WalksController(IWalksRepositorycs walksRepositorycs, IMapper mapper)
        {
            this.walksRepositorycs = walksRepositorycs;
            this.mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalksDTO addWalksDTO)
        {
            if (ModelState.IsValid)
            {
                var walk = mapper.Map<Walk>(addWalksDTO);
                await walksRepositorycs.CreateAsync(walk);

                return Ok(mapper.Map<WalksDTO>(walk));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] string? FilterOn, [FromQuery] string? FilterBy,
            [FromQuery] string? sortBy, [FromQuery] bool isAsc, [FromQuery] int pageNbr, [FromQuery] int pageSize)
        {
                  
           var walks = await walksRepositorycs.GetAllAsync(FilterOn,FilterBy,sortBy, isAsc, pageNbr, pageSize);

           throw new  Exception("This is a new exception:");

           var walksDTO = mapper.Map<List<WalksDTO>>(walks);

          return Ok(walksDTO); 
        }

        [HttpGet]
        [Route("{id:GUID}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var walk = await walksRepositorycs.GetByIdAsync(id);
            
            if(walk == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalksDTO>(walk));

        }


        [HttpPut]
        [Route("{id:GUID}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateWalkDTO updateWalkDTO)
        {
            var walk = mapper.Map<Walk>(updateWalkDTO);
                        
             walk = await walksRepositorycs.UpdateAsync(id, walk);

            if(walk == null)
            { 
                return NotFound();
            }

            return Ok(mapper.Map<UpdateWalkDTO>(walk));
        }


        [HttpDelete]
        [Route("{id:GUID}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var walk =await walksRepositorycs.DeleteAsync(id);

            if (walk == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalksDTO>(walk));
        }
    }
}
