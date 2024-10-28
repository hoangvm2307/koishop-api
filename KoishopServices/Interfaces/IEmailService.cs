using KoishopBusinessObjects;

namespace UberSystem.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailVerificationAsync(string? email, List<KoiFish> list, Order order);
    }
}