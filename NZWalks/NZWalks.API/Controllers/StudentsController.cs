using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //GET: Https://locatlhost:portnumber/api/students
        [HttpGet]
        public IActionResult GetAllStudents()

        {
            string[] studentNames = new string[] { "Holy", "John", "Owais" };

            return Ok(studentNames);
        }
    }
}
