
using DTOs.AccountDtos;

namespace KoishopServices
{
  public interface IAccountService
  {
     Task<UserDto> Login(LoginDto loginDto);
  }
}