using EventBooking.API;
using EventBooking.API.Middlewares;
using EventBooking.Application;
using EventBooking.Domain.Entities;
using EventBooking.Infrastructure;
using EventBooking.Infrastructure.Persistences.DBContext;
using EventBooking.Infrastructure.Persistences.SeedData;
using EventBooking.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(p =>
               p.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
               /*.EnableSensitiveDataLogging());*/ // Ghi log quá trình database

builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
// Add other configure
builder.Services.ConfigureApiServices(builder.Configuration);
builder.Services.ConfigureApplicationService(builder.Configuration);
builder.Services.ConfigureInfrastructureService(builder.Configuration);
//builder.Services.AddRabbitMq(configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        var dbContextInitialiser = services.GetRequiredService<ApplicationDbContextInitialiser>();
        dbContextInitialiser.InitialiseAsync().Wait();
        dbContextInitialiser.SeedAsync().Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{   
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ValidationMiddleware>();
app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.MapControllers();

app.Run();
