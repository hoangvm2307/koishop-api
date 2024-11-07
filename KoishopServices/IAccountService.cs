using DTOs.AccountDtos;
using KoishopBusinessObjects;

namespace KoishopServices
{
    public interface IAccountService
    {
        Task<UserDto> Login(LoginDto loginDto);
        Task<UserDto> Register(RegisterDto registerDto);
        Task<List<UserDto>> GetUsers();
    }
}