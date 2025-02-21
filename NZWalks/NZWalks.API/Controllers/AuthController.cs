using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        //POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            IdentityUser user = new IdentityUser()
            {
                UserName = registerRequestDTO.UserName,
                Email = registerRequestDTO.UserName
            };

            var identityResult = await userManager.CreateAsync(user, registerRequestDTO.Password);

            if (identityResult.Succeeded)
            {
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(user, registerRequestDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User registered successfully");
                    }
                    return BadRequest("Error in setting the role");
                }

            }
            return BadRequest("Error in creating a user");
        }


        //POST:/api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var loginUser = await userManager.FindByEmailAsync(loginRequestDTO.UserName);

            if (loginUser != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(loginUser, loginRequestDTO.Password);

                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(loginUser);

                    if (roles != null)
                    {
                        //Create Token
                        var jwtToken = tokenRepository.CreateJWTToken(loginUser,roles.ToList());

                        LoginResponseDTO responseDTO = new LoginResponseDTO
                        {
                            JwtToken = jwtToken,
                        };

                        return Ok(responseDTO);
                    }
                                        
                }
                return BadRequest("Invalid User ID or Pwd");
            }
            return BadRequest("Invalid UserName");
        }
    }
}
