namespace KoishopServices.Common.Interface
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        Task<bool> IsInRoleAsync(string role);
        Task<bool> AuthorizeAsync(string policy);
    }
}
