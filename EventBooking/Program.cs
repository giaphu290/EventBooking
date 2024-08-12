using EventBooking.API;
using EventBooking.API.Controllers.Middlewares;
using EventBooking.Application;
using EventBooking.Domain.Entities;
using EventBooking.Infrastructure;
using EventBooking.Infrastructure.Persistences.DBContext;
using EventBooking.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(p =>
               p.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
// Add other configure
builder.Services.ConfigureApiServices(builder.Configuration);
builder.Services.ConfigureApplicationService(builder.Configuration);
builder.Services.ConfigureInfrastructureService(builder.Configuration);
//builder.Services.AddRabbitMq(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseStaticFiles();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ValidationMiddleware>();
app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.MapControllers();

app.Run();
