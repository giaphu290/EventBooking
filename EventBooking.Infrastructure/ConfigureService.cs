using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Domain.Entities;
using EventBooking.Domain.Private;
using EventBooking.Infrastructure.Persistences.DBContext;
using EventBooking.Infrastructure.Persistences.Repositories;
using EventBooking.Infrastructure.Persistences.SeedData;
using EventBooking.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Infrastructure
{
    public static class ConfigureService
    {
        public static IServiceCollection ConfigureInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>(); // Đăng ký UnitOfWork để quản lý các phiên làm việc với cơ sở dữ liệu
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<ITimeService, TimeService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<INormalizeVietnamese, NormalizeVietnameseService>();
            services.AddScoped<IMediatorService, MediatorService>();
            services.AddIdentityCore<User>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>(); 
            services.AddScoped<ApplicationDbContextInitialiser>();
            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = true; 
                options.Password.RequireLowercase = true; 
                options.Password.RequireNonAlphanumeric = false; 
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6; 
                options.Password.RequiredUniqueChars = 1; 
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();
            PasswordGenerator.Initialize(configuration);
                  
            return services;
        }
    }
}
