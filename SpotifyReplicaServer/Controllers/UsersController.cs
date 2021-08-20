using AutoMapper;
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
        private readonly IUserService userService;
       private readonly IMapper mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("login")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = userService.Login(model);

            if (response == null)
                return NotFound(new { message = "شناسه‌کاربری و یا رمز عبور اشتباه می‌باشد." });

            return Ok(new { token = response });
        }

        [HttpPost("register")]
        public IActionResult Register(UserViewModel userViewModel)
        {
            var user = mapper.Map<User>(userViewModel);

            var response = this.userService.Register(user);

            if (response.Result == null)
            {
                return BadRequest(new { message = "شناسه‌کاربری و یا ایمیل تکراری می‌باشد." });
            }

            return Ok(new { token = response.Result });
        }

        [Authorize]
        [HttpPut("alter")]
        public IActionResult Alter(UserViewModel userViewModel)
        {
            var user = mapper.Map<User>(userViewModel);

            var response = this.userService.Alter(user);

            if (!string.IsNullOrEmpty(response.Result))
            {
                return BadRequest(new { message = response.Result });
            }

            return Ok(new { message = "اطلاعات با موفقیت ذخیره شد." });
        }

        [Authorize]
        [HttpGet("user")]
        public IActionResult GetUser()
        {
            var user = userService.GetUser();
            user.Password = null;

            return Ok(user);
        }
    }
}
