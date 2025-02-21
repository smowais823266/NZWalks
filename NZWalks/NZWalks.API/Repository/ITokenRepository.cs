using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repository
{
    public interface ITokenRepository
    {
        public string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
