using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskManager.Application.Behaviours;
using TaskManager.Application.Contracts.Application;
using TaskManager.Application.Services;

namespace TaskManager.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection
            services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IPermissionService, PermissionService>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
