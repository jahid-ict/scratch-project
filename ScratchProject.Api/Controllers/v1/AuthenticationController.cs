using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using ScratchProject.Api.DataTransferObjects;
using ScratchProject.Api.Intefaces;
using System.Security.Claims;

namespace ScratchProject.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [AllowAnonymous]
    public class AuthenticationController : CustomControllerBase
    {
        private IJwtService _jwtService { get; set; }
        public AuthenticationController(IJwtService jwtService) 
        {
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult GenerateUserToken(UserAuthenticationRequest
            userRequest)
        {
            return Ok(_jwtService.GenerateAuthenticationToken(userRequest));
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult SignUp(UserSignUpRequest UserSignUpRequest)
        {
            return Ok(_jwtService.GenerateAuthenticationToken(new UserAuthenticationRequest()));
        }


        [HttpPost("generate-new-access-token")]
        public IActionResult GenerateNewAccessToken(TokenModel tokenModel)
        {
            ClaimsPrincipal principal = _jwtService.GetPrincipalFromJwtToken(tokenModel.Token);
            if(principal == null)
            {
                return BadRequest("Invalid jwt access token");
            }

            var userEmail = principal.FindFirstValue(ClaimTypes.Email);
            if(string.IsNullOrEmpty(userEmail)) {
                return BadRequest("Invalid jwt access token");
            }
            return Ok(_jwtService.GenerateAuthenticationToken(new UserAuthenticationRequest { Email = userEmail}));
        }
    }
}
