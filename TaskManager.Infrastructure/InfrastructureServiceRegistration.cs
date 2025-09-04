using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Contracts.Infrastructure;

namespace TaskManager.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
