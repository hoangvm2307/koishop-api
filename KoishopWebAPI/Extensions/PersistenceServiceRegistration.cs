using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using KoishopServices;
using KoishopServices.Common.Interface;
using KoishopWebAPI.Service;

namespace KoishopWebAPI.Extensions
{
  public static class PersistenceServiceRegistration
  {
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddScoped<IAccountService, AccountService>();
      services.AddScoped<ICurrentUserService, CurrentUserService>();  
      return services;
    }
  }
}