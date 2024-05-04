using ScratchProject.Api.DataTransferObjects;
using System.Security.Claims;

namespace ScratchProject.Api.Intefaces
{
    public interface IJwtService
    {
        public UserAuthenticationSuccessResponse GenerateAuthenticationToken(UserAuthenticationRequest userRequest);
        public ClaimsPrincipal GetPrincipalFromJwtToken(string token);
    }
}
