using DTOs.AccountDtos;
using KoishopBusinessObjects;
using KoishopRepositories;
using KoishopServices.Common.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace KoishopServices
{
     public class AccountService : IAccountService
     {
        private readonly UserManager<User> _userManager;
        private readonly TokenService _tokenService;
        private readonly KoishopDBContext _context;

        public AccountService(KoishopDBContext context, UserManager<User> userManager, TokenService tokenService)
        {
            _context = context;
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
                PhoneNumber = registerDto.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                throw new Exception("User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            await _userManager.AddToRoleAsync(user, "Customer");
            _context.SaveChanges();
            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                User = user
            };
        }

        public async Task<List<UserDto>> GetUsers()
        {
            var users = await _userManager.GetUsersInRoleAsync("Customer");

            var userDtos = users.Select(user => new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
            }).ToList();

            return userDtos;
        }
    }
}