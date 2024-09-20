using DTOs.AccountDtos;
using KoishopBusinessObjects;
using Microsoft.AspNetCore.Identity;

namespace KoishopServices
{
    public class AccountService : IAccountService
    {
        // public AccountService(UserManager<User> userManager, TokenService tokenService)
        // public Task<UserDto> Login(string username, string password)
        // {
        //     var user = await _userManager.FindByNameAsync(loginDto.Username);
        //     if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
        //         return Unauthorized();

        //     var userBasket = await RetrieveBasket(loginDto.Username);
        //     var anonBasket = await RetrieveBasket(Request.Cookies["buyerId"]);

        //     if (anonBasket != null)
        //     {
        //         if (userBasket != null) _context.Baskets.Remove(userBasket);
        //         anonBasket.BuyerId = user.UserName;
        //         Response.Cookies.Delete("buyerId");
        //         await _context.SaveChangesAsync();
        //     }

        //     return new UserDto
        //     {
        //         Email = user.Email,
        //         Token = await _tokenService.GenerateToken(user),
        //         Basket = anonBasket != null ? anonBasket.MapBasketToDto() : userBasket?.MapBasketToDto()
        //     };
        // }
        public Task<UserDto> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> Login(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }
    }
}