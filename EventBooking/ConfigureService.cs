using Microsoft.OpenApi.Models;
using System.Reflection;

namespace EventBooking.API
{
    public static class ConfigureService
    {
        public static IServiceCollection ConfigureApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            //services.AddScoped<IEmailService, EmailService>();

            //services.ConfigCors();
            //services.ConfigSwagger();
            //services.RabbitMq(configuration);
            //services.Quartz();

            return services;
        }

        //        public static void ConfigCors(this IServiceCollection services)
        //        {
        //            services.AddCors(options =>
        //            {
        //                options.AddPolicy("CorsPolicy",
        //                    builder =>
        //                    {
        //                        builder.WithOrigins("*")
        //                               .AllowAnyHeader()
        //                               .AllowAnyMethod()
        //                               .AllowCredentials();
        //                    });
        //            });
        //        }

        //        //public static void AddAuthenJwt(this IServiceCollection services, IConfiguration configuration)
        //        //{
        //        //    var jwtSettings = configuration.GetSection("JwtSettings");
        //        //    services.AddAuthentication(options =>
        //        //    {
        //        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //        //    }).AddJwtBearer(options =>
        //        //    {
        //        //        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        //        //        {
        //        //            ValidateIssuer = true,
        //        //            ValidateAudience = true,
        //        //            ValidateIssuerSigningKey = true,
        //        //            ValidateLifetime = true,
        //        //            ValidIssuer = jwtSettings["Issuer"],
        //        //            ValidAudience = jwtSettings["Audience"],
        //        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
        //        //        };
        //        //        options.SaveToken = true;
        //        //        options.RequireHttpsMetadata = true;
        //        //        options.Events = new JwtBearerEvents();
        //        //    });
        //        //}

        public static void ConfigSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header sử dụng scheme Bearer.",
                    Type = SecuritySchemeType.Http,
                    Name = "Authorization",
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    },
                                    Scheme = "oauth2",
                                    Name = "Bearer",
                                    In = ParameterLocation.Header
                                },
                                new List<string>()
                            }
                });
            });
        }

        //        //public static void RabbitMq(this IServiceCollection services, IConfiguration configuration)
        //        //{
        //        //    var rabbitMqSettings = configuration.GetSection("RabbitMQ");
        //        //    if (!int.TryParse(rabbitMqSettings["Port"], out int port))
        //        //    {
        //        //        throw new InvalidOperationException("Invalid port number in RabbitMQ configuration.");
        //        //    }
        //        //    services.AddSingleton(new RabbitMQHelper(
        //        //        hostName: rabbitMqSettings["HostName"],
        //        //        username: rabbitMqSettings["UserName"],
        //        //        password: rabbitMqSettings["Password"],
        //        //        managementUrl: rabbitMqSettings["ManagementUrl"],
        //        //        port: port
        //        //    ));
        //        //    services.AddTransient<IEmailService, EmailService>();
        //        //}

        //        //public static void Quartz(this IServiceCollection services)
        //        //{
        //        //    services.AddSingleton<NotificationConsumer>();
        //        //    services.AddHostedService<NotificationConsumerService>();
        //        //}
    }
    }

