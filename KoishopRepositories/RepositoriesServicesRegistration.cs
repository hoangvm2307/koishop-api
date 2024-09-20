﻿using KoishopRepositories.Interfaces;
using KoishopRepositories.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        return services;
    }
}
