using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTOs.AccountDtos;
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
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
    {
      if (loginDto == null || !ModelState.IsValid) return BadRequest("Invalid login request");

      var user = await _accountService.Login(loginDto);

      if (user == null) return Unauthorized("Invalid user name or password");

      Response.Cookies.Append("token", user.Token, new CookieOptions { Secure = true, SameSite = SameSiteMode.None, Expires = DateTime.Now.AddDays(7) });

      return user;
    }
  }
}