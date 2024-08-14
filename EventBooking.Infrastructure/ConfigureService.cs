using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Domain.Entities;
using EventBooking.Domain.Private;
using EventBooking.Infrastructure.Persistences.DBContext;
using EventBooking.Infrastructure.Persistences.Repositories;
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
            //services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMediatorService, MediatorService>();
            services.AddIdentityCore<User>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>(); // Đăng ký Identity và Entity Framework

            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = true; // Mật khẩu phải chứa ít nhất một ký tự số
                options.Password.RequireLowercase = true; // Mật khẩu phải chứa ít nhất một ký tự chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không yêu cầu ký tự không phải là chữ hoặc số
                options.Password.RequireUppercase = true; // Mật khẩu phải chứa ít nhất một ký tự chữ hoa
                options.Password.RequiredLength = 6; // Độ dài tối thiểu của mật khẩu
                options.Password.RequiredUniqueChars = 1; // Số lượng ký tự khác nhau tối thiểu trong mật khẩu
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();
            PasswordGenerator.Initialize(configuration);
                   

            //Quartz
            //services.AddQuartz(options =>
            //{
            //    options.UseMicrosoftDependencyInjectionJobFactory();
            //});
            //services.AddQuartzHostedService(options =>
            //{
            //    options.WaitForJobsToComplete = true;
            //});
            //services.ConfigureOptions<QuartzSetup>();

            return services;
        }
    }
}
