using KoishopRepositories.Interfaces;
using KoishopRepositories.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KoishopRepositories;

public static class RepositoriesServicesRegistration
{
    public static IServiceCollection AddRepositoriesServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext.KoishopContext>(options => {
            options.UseNpgsql(configuration.GetConnectionString("ConnectionString"));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IBreedRepository, BreedRepository>();
        services.AddScoped<IConsignmentRepository, ConsignmentRepository>();
        services.AddScoped<IConsignmentItemRepository, ConsignmentItemRepository>();
        services.AddScoped<IFishCareRepository, FishCareRepository>();
        services.AddScoped<IKoiFishRepository, KoiFishRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IRatingRepository, RatingRepository>();

        return services;
    }
}
