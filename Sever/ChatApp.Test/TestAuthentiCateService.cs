using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Interfaces.Services;
using ChatApp.Application.Requests.Users;
using ChatApp.Infrastructure.Contexts;
using ChatApp.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Test
{
    [TestClass]   
    public class TestAuthentiCateService
    {
        [TestMethod]
        public async Task TestCreateUser()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .AddEnvironmentVariables()
                .Build();

            ServiceCollection service = new ServiceCollection();
            service.AddSingleton(configuration);
            service.AddScoped<IDbFactory, DbFactory>();
            service.AddScoped<IDateTimeProvider, DateTimeProvider>();
            service.AddScoped<IPasswordHasher, PasswordHasher>();
            service.AddScoped<IJwtGenerator, JwtGenerator>();
            service.AddScoped<IAuthenticateService, AuthenticateService>();
            var provider = service.BuildServiceProvider();
            var authenticateService = provider.CreateScope().ServiceProvider.GetRequiredService<IAuthenticateService>();
            var result = await authenticateService.LoginOrRegister(new UserForLoginOrRegister("buibong2912", "29122002Az@"));
            Assert.AreEqual(result.Success, true, "Create User Failed");
        }
    }
}
