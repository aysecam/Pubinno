using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pubinno.Application.Interfaces.Repositories;
using Pubinno.Application.Interfaces.Services;
using Pubinno.Persistence.Context;
using Pubinno.Persistence.Repositories;

namespace Pubinno.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<PubinnoDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(EfRepositoryBase<>));
            services.AddScoped<IPourRepository, PourRepository>();
            services.AddScoped<IExceptionLogService, ExceptionLogRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILoggerService, LoggerService>();

            return services;
        }
    }
}
