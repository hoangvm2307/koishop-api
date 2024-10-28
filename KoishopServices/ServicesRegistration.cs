using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using KoishopServices.Interfaces;
using KoishopServices.Services;
using KoishopServices.Interfaces.Third_Party;
using UberSystem.Domain.Interfaces.Services;
using UberSystem.Service;

namespace KoishopServices;

public static class ServicesRegistration
{
    public static IServiceCollection AddServicesServices(this IServiceCollection services)
    {
        //TODO: ADD THIS CODE IF HAVE BUG
        //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IBreedService, BreedService>();
        services.AddScoped<IConsignmentItemService, ConsignmentItemService>();
        services.AddScoped<IConsignmentService, ConsignmentService>();
        services.AddScoped<IFishCareService, FishCareService>();
        services.AddScoped<IKoiFishService, KoiFishService>();
        services.AddScoped<IOrderItemService, OrderItemService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IRatingService, RatingService>();
        services.AddScoped<IVnPayService, VnPayService>();
        services.AddScoped<IEmailService, EmailService>();
        return services;
    }
}
