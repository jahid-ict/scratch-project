using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScratchProject.Api.DataTransferObjects;
using ScratchProject.Api.Intefaces;
using ScratchProject.Api.Models;

namespace ScratchProject.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [AllowAnonymous]
    public class MongoDBController : CustomControllerBase
    {
        private readonly IMongoDbClientService _mongoDbClientService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        //private readonly ILogger<MongoDBController> _logger;

        public MongoDBController(IMongoDbClientService mongoDbClientService,
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration)
        {
            _mongoDbClientService = mongoDbClientService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        [HttpGet("[action]")]
        public IActionResult GetItemByTitle(string title)
        {
            var item = _mongoDbClientService.Get(title);
            return Ok(item);
        }

        [HttpGet("[action]")]
        public IActionResult GetUserByEmail(string email)
        {
            var userModel = _mongoDbClientService.GetItemByField<UserModel>("Email", email);
            return Ok(userModel);
        }

        [HttpPost("[action]")]
        public IActionResult AddUser(UserAuthenticationRequest user)
        {
            //_logger.LogDebug($"Add user started with {user.Email}");
            var config = _configuration["Config"];
            var userModel = new UserModel { Email = user.Email, FirstName = user.FirstName, LastName = user.LastName };
            _mongoDbClientService.Save(userModel);
            //_logger.LogDebug($"Add user completed with {user.Email}");
            return Ok(userModel);
        }
    }
}
