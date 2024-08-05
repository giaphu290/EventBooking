using EventBooking.API;
using EventBooking.Application;
using EventBooking.Domain.Entities;
using EventBooking.Infrastructure;
using EventBooking.Infrastructure.Persistences.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add database
builder.Services.AddDbContext<ApplicationDbContext>(p =>
    p.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
// Add other configure
builder.Services.ConfigureApiServices(builder.Configuration);
builder.Services.ConfigureApplicationService(builder.Configuration);
builder.Services.ConfigureInfrastructureService(builder.Configuration);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
