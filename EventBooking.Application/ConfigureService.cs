using EventBooking.Application.Common.Behaviors;
using EventBooking.Application.Common.Mapping;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.Auth.LoginManage.Commands;
using EventBooking.Application.Features.Auth.LoginManage.Handlers;
using EventBooking.Application.Features.Auth.LoginManage.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using FluentValidation;

namespace EventBooking.Application
{
    public static class ConfigureService
    {
        public static IServiceCollection ConfigureApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationErrorBehaviour<,>));
            });
            //PasswordGenerator.Initialize(configuration);
            services.AddSingleton<TokenValidationParameters>(provider =>
            {
                return new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = configuration["Jwt:Issuer"],
                    ValidIssuer = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                    ClockSkew = TimeSpan.FromMinutes(60)
                };
            });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Thắc mắc
            services.AddTransient<IRequestHandler<LoginCommand, LoginResponse>, LoginHandler>();
            services.AddTransient<IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>, RefreshTokenHandler>();
            //services.AddTransient<IRequestHandler<SendMessageCommand, bool>, SendMessageHandler>();

            //services.AddSignalR();
            return services;
        }
    }
}
