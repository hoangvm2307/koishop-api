using KoishopBusinessObjects;

namespace DTOs.AccountDtos
{
  public class UserDto
  {
    public string Email { get; set; }
    public string Token { get; set; }
    public User User { get; set; }
    // public BasketDto Basket {get;set;}
  }
}