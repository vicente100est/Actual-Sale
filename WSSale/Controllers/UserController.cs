using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSSale.Models.Request;
using WSSale.Models.Response;
using WSSale.Services;

namespace WSSale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Autheticate([FromBody] AuthRequest model)
        {
            Response modelResponse = new Response();

            var userResponse = _userService.Auth(model);

            if(userResponse == null)
            {
                modelResponse.Message = "User or password is not correct";
                return BadRequest(modelResponse);
            }

            modelResponse.Success = 1;
            modelResponse.Data = userResponse;

            return Ok(modelResponse);
        }
    }
}
