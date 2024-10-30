using Microsoft.AspNetCore.Identity;

namespace KoishopBusinessObjects
{
    public class User : IdentityUser<int>
    {
        public decimal Wallet { get; set; } = 0;
    }
}