using DTOs.AccountDtos;
using KoishopBusinessObjects;

namespace KoishopServices
{
    public interface IAccountService
    {
        Task<UserDto> Login(LoginDto loginDto);
        Task<UserDto> Register(RegisterDto registerDto);
        Task<UserDto> AddStaff(RegisterDto registerDto);
        Task<List<UserDto>> GetUsers();
        Task<List<UserDto>> GetStaffs();
        Task<User> UpdateUserAsync(int id, UserUpdateDto userDto);
        Task<bool> RemoveUser(string id);
    }
}