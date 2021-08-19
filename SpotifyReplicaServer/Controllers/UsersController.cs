using Microsoft.AspNetCore.Mvc;
using SpotifyReplicaServer.Abstraction;
using SpotifyReplicaServer.Business.Athentication;
using SpotifyReplicaServer.Models;
using SpotifyReplicaServer.Models.Request;
using SpotifyReplicaServer.Models.Transfer;
using System;

namespace SpotifyReplicaServer.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("login")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = userService.Login(model);

            if (response == null)
                return BadRequest(new { message = "شناسه‌کاربری و یا رمز عبور اشتباه می‌باشد." });

            return Ok(new { token = response });
        }

        [HttpPost("register")]
        public IActionResult Register(UserDto userDto)
        {
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Password = userDto.Password,
                Username = userDto.UserName,
                Email = userDto.Email
            };

            var response = this.userService.Register(user);

            if (response.Result == null)
            {
                return BadRequest(new { message = "شناسه‌کاربری و یا ایمیل تکراری می‌باشد." });
            }

            return Ok(new { token = response.Result });
        }

        [Authorize]
        [HttpPut("alter")]
        public IActionResult Alter(UserDto userDto)
        {
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Password = userDto.Password,
                Username = userDto.UserName,
                Email = userDto.Email
            };

            var response = this.userService.Alter(user);
            
            if (!response.Result)
            {
                return BadRequest(new { message = "وارد کردن حداقل یک مشخصه الزامیست." });
            }

            return Ok(new { message= "اطلاعات با موفقیت ذخیره شد."});
        }

        [Authorize]
        [HttpGet("user")]
        public IActionResult GetUser()
        {
            var user = userService.GetUser();

            var newUser = new User
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = null,
            };

            return Ok(newUser);
        }
    }
}
