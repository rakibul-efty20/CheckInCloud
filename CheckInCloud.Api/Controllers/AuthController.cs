using CheckInCloud.Api.Constants;
using CheckInCloud.Api.Contracts;
using CheckInCloud.Api.Data;
using CheckInCloud.Api.DTOs.Auth;
using CheckInCloud.Api.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CheckInCloud.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController(IUsersService usersService) : BaseApiController
    {
       
        [HttpPost("register")]
        public async Task<ActionResult<RegisteredUserDto>> Register(RegisterUserDto registerUserDto)
        {
           var result = await usersService.RegisterAsync(registerUserDto);
           return ToActionResult(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginUserDto loginUserDto)
        {
            var result = await usersService.LoginAsync(loginUserDto);
            return ToActionResult(result);
        }
    }
}
//messi125@gmail.com
//#Messi1235