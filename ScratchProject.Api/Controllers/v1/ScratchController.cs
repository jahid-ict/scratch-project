using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ScratchProject.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ScratchController : CustomControllerBase
    {
        [HttpGet]
        [Route("gethelloworldmessage")]
        public IActionResult GetHelloWorld()
        {
            var helloWorldMessage = "Hello world!";
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
            var helloWorldMessage = "Hello world alternate!";
            return helloWorldMessage;
        }
    }
}
