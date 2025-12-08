using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
   public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
   {
      Assembly assembly = typeof(DependencyInjection).Assembly;
      services.AddMediatR(mediatConfiguration => 
         mediatConfiguration.RegisterServicesFromAssembly(assembly));
      return services;
   }
}