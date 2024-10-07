using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using KoishopServices.Interfaces;
using KoishopServices.Services;

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

        return services;
    }
}
