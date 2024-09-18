using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using KoishopServices;

namespace KoishopWebAPI.Extensions
{
  public static class PersistenceServiceRegistration
  {
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddScoped<IAccountService, AccountService>();
      return services;
    }
  }
}