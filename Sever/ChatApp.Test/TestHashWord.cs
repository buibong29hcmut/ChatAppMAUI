using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Interfaces.Services;
using ChatApp.Infrastructure.Contexts;
using ChatApp.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Test
{
    [TestClass]

    public class TestHashWord
    {
        [TestMethod]
        public void TestHash()
        {
            ServiceCollection service = new ServiceCollection();
        
            service.AddScoped<IDbFactory, DbFactory>();
            service.AddScoped<IDateTimeProvider, DateTimeProvider>();
            service.AddScoped<IPasswordHasher, PasswordHasher>();
            var provider = service.BuildServiceProvider();
            var hasher = provider.CreateScope().ServiceProvider.GetRequiredService<IPasswordHasher>();
            var result = hasher.HashWithSHA256Algo("29122002Az@");
            bool check = hasher.CheckPassWord("29122002Az@", result.PasswordHash, result.Salt);
            Assert.AreEqual(check, true, "Password Hash Equal");
        }
    }
}
