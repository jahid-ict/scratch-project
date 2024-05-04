using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScratchProject.Api.DataTransferObjects;
using ScratchProject.Api.Intefaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ScratchProject.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [AllowAnonymous]
    public class RedisController : CustomControllerBase
    {
        private readonly IRedisService _redisService;
        public RedisController(IRedisService redisService) 
        {
            _redisService = redisService;
        }

        [HttpGet("[action]")]
        public IActionResult RedisGet(string key)
        {
            return Ok(_redisService.GetString(key));
        }

        [HttpPost("[action]")]
        public IActionResult RedisPost(string key, CarModel value)
        {
            _redisService.SaveString(key, JsonSerializer.Serialize(value));
            return Ok();
        }
    }
}
