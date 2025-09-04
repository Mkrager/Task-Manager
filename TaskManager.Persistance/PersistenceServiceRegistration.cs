using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Contracts.Persistance;
using TaskManager.Persistance.Repositories;

namespace TaskManager.Persistance
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this
            IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskManagerDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString
            ("TaskManagerConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
           
            return services;
        }
    }
}
