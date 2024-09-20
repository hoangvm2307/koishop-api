
using DTOs.AccountDtos;

namespace KoishopServices
{
  public interface IAccountService
  {
     Task<UserDto> Login(string username, string password);
     Task<UserDto> Login(LoginDto loginDto);
    }
}