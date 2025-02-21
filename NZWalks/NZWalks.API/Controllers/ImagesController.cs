using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO requestDTO)
        {
            ValidateFile(requestDTO);

            if (ModelState.IsValid)
            {

                var image = new Image()
                {
                    File = requestDTO.File,
                    FileName = requestDTO.FileName,
                    FileExtension = Path.GetExtension(requestDTO.File.FileName),
                    FileDescription = requestDTO.FileDescription,
                    FileSizeInBytes = requestDTO.File.Length,
                };

                await imageRepository.Upload(image);

                return Ok(image);
            }
            return BadRequest(ModelState);
       
        }

        private void ValidateFile(ImageUploadRequestDTO requestDTO)
        {
            var validExtension = new string[] { ".png", ".jpg" };

            if (!validExtension.Contains(Path.GetExtension(requestDTO.File.FileName)))
            {
                ModelState.AddModelError("File", "Invalid Image Extension");
            }

            if(requestDTO.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "Image Size is too big");
            }
        }

    }

}
