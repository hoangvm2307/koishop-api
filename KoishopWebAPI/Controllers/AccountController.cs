using DTOs.AccountDtos;
using KoishopBusinessObjects;
using KoishopServices;
using Microsoft.AspNetCore.Mvc;

namespace KoishopWebAPI.Controllers
{
    public class AccountController : BaseApiController
  {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        /// <summary>
        /// Login to the system
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null || !ModelState.IsValid) return BadRequest("Invalid login request");

            var user = await _accountService.Login(loginDto);

            if (user == null) return Unauthorized("Invalid user name or password");

            Response.Cookies.Append("token", user.Token, new CookieOptions { Secure = true, SameSite = SameSiteMode.None, Expires = DateTime.Now.AddDays(7) });

            return user;
        }

        /// <summary>
        /// Sign up to the system via username/email
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            if (registerDto == null || !ModelState.IsValid) return BadRequest("Invalid register request");

            var user = await _accountService.Register(registerDto);

            Response.Cookies.Append("token", user.Token, new CookieOptions { Secure = true, SameSite = SameSiteMode.None, Expires = DateTime.Now.AddDays(7) });

            return user;
        }

        /// <summary>
        /// Logout from the system
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        public ActionResult<JsonResponse<string>> Logout()
        {
            Response.Cookies.Append("token", "", new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1),
                Secure = true,
                SameSite = SameSiteMode.None
            });

            return Ok(new JsonResponse<string>("Logged out successfully"));
        }

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            return await _accountService.GetUsers();
        }
    }
}