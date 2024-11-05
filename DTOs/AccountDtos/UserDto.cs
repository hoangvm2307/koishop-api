using KoishopBusinessObjects;

namespace DTOs.AccountDtos
{
  public class UserDto
  {
    public int Id { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public User User { get; set; }
    public string UserName { get; set; }
        // public BasketDto Basket {get;set;}
    }
}