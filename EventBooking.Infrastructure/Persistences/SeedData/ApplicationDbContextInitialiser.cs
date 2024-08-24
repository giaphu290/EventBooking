using EventBooking.Application.Features.Auth.UserManage.Models;
using EventBooking.Domain.Entities;
using EventBooking.Infrastructure.Persistences.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Infrastructure.Persistences.SeedData
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    if (!_context.Database.CanConnect())
                    {
                        await _context.Database.EnsureDeletedAsync();
                        await _context.Database.MigrateAsync();
                    }
                    else
                    {
                        await _context.Database.MigrateAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initializing the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }
        private async Task TrySeedAsync()
        {
            await SeedRolesAsync();
            await SeedUsersAsync();

        }
        #region roles
        private async Task SeedRolesAsync()
        {
            var roles = new[]
            {
                new IdentityRole { Name = "Admin" },
                new IdentityRole { Name = "Guest" },
                new IdentityRole { Name = "Host" },
            };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role.Name))
                {
                    await _roleManager.CreateAsync(role);
                }
            }
        }
        #endregion roles
        #region users
        private async Task SeedUsersAsync()
        {
            var users = new[]
     {
        new { UserName = "admin", Email = "admin@example.com", Role = "Admin", Password = "Admin12345" },
        new { UserName = "host", Email = "host@example.com", Role = "Host", Password = "Host12345" },
        new { UserName = "guest", Email = "guest@example.com", Role = "Guest", Password = "Guest12345" }
    };

            foreach (var userInfo in users)
            {
                if (await _userManager.FindByNameAsync(userInfo.UserName) == null)
                {
                    var user = new User
                    {
                        Name = userInfo.UserName,
                        UserName = userInfo.UserName,
                        Email = userInfo.Email,
                        EmailConfirmed = true
                    };

                    var result = await _userManager.CreateAsync(user, userInfo.Password);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation($"User {userInfo.UserName} created successfully.");
                        await _userManager.AddToRoleAsync(user, userInfo.Role);
                        await _context.SaveChangesAsync();
                        _logger.LogInformation($"UserProfile for {userInfo.UserName} added to database.");
                    }
                    else
                    {
                        _logger.LogError($"Failed to create user {userInfo.UserName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
                else
                {
                    _logger.LogInformation($"User {userInfo.UserName} already exists.");
                }
            }
        }
        #endregion users
    }
}
