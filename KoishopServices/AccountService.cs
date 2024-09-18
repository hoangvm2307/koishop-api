using DTOs.AccountDtos;
using KoishopBusinessObjects;
using Microsoft.AspNetCore.Identity;

namespace KoishopServices
{
  public class AccountService : IAccountService
  {
    private readonly UserManager<User> _userManager;
    private readonly TokenService _tokenService;

    public AccountService(UserManager<User> userManager, TokenService tokenService)
    {
      _userManager = userManager;
      _tokenService = tokenService;
    }
    public async Task<UserDto> Login(LoginDto loginDto)
    {
      var user = await _userManager.FindByNameAsync(loginDto.Username);
      if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
        return null;

      return new UserDto
      {
        Email = user.Email,
        Token = await _tokenService.GenerateToken(user),
        User = user
      };
    }
    
  }
}