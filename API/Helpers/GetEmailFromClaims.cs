using System.Linq;
using System.Security.Claims;

namespace API.Helpers
{
    public static class GetEmailFromClaims
    {
        public static string GetEmail(ClaimsPrincipal userClaims)
        {
            var email = userClaims.Claims.
                FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            return email;
        }
        
    }
}