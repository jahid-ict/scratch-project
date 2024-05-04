using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ScratchProject.Api.Controllers.v2
{
    [ApiVersion("2.0")]
    public class ScratchController : CustomControllerBase
    {
        [HttpGet]
        [Route("gethelloworldmessage")]
        public IActionResult GetHelloWorld()
        {
            var helloWorldMessage = "Hello world v2!";
            return Ok(helloWorldMessage);
        }

        /// <summary>
        // Get hello world in alternative way
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [Produces("application/json")]
        public ActionResult<string> GetHelloWorldAlternate()
        {
            var helloWorldMessage = "Hello world alternate v2 !";
            return helloWorldMessage;
        }
    }
}
