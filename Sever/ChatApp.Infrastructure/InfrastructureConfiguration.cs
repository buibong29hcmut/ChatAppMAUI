using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Interfaces.Services;
using ChatApp.Infrastructure.Contexts;
using ChatApp.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ChatApp.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, params Type[] types)
        {

            var assemblies = types.Select(type => type.GetTypeInfo().Assembly);

            foreach (var assembly in assemblies)
            {
                services.AddMediatR(assembly);
            }
            var connectionString = configuration.GetConnectionString("ChatDb");
            services.AddDbContext<ChatDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
                options.EnableSensitiveDataLogging(true);
            });
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
            });
            services.AddScoped<IChatDbContext>(provider=>provider.GetRequiredService<ChatDbContext>());
            services.AddScoped<IDbFactory, DbFactory>();
            services.AddScoped<IUserOperationInMemmory, UserOperationInMemmory>();
            services.AddScoped<IUserOperation, UserOperation>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IUploadFileToAzureBlobService, UploadFileToAzureBlobService>();
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            return services;
        }

    }
}