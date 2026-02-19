using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Pubinno.Application.Common.Behaviors.Logging;
using Pubinno.Application.Common.Behaviors.Transaction;
using Pubinno.Application.Common.Behaviors.Validation;

namespace Pubinno.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = typeof(ApplicationServiceRegistration).Assembly;

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
            });

            // Sıralama önemli: Logging → Validation → Transaction → Handler
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionScopeBehavior<,>));

            services.AddValidatorsFromAssembly(assembly);
            services.AddAutoMapper(cfg => { }, assembly);

            return services;
        }
    }
}