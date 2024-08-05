using EventBooking.Application.Common.Behaviors;
using EventBooking.Application.Common.Mapping;
using EventBooking.Application.Common.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EventBooking.Application
{
    public static class ConfigureService
    {
        public static IServiceCollection ConfigureApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationErrorBehaviour<,>));

            });
            return services;
        }
    }
}
