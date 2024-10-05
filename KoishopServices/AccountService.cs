using DTOs.AccountDtos;
using KoishopBusinessObjects;
using KoishopServices.Common.Exceptions;
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
            {
                return null;
            }

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                User = user
            };
        }
        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            var existuser = await _userManager.FindByNameAsync(registerDto.UserName);
            if (existuser != null)
            {
                throw new DuplicationException("User is already exist");
            }
            var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber
            };
            user.SecurityStamp = Guid.NewGuid().ToString();
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
            }

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                User = user
            };
        }

    }
}