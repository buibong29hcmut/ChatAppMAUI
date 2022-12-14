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
            ServiceCollectionContainer container = new ServiceCollectionContainer();
           
            var provider = container.ServiceCollection().BuildServiceProvider();
            var authenticateService = provider.CreateScope().ServiceProvider.GetRequiredService<IAuthenticateService>();
            var result = await authenticateService.LoginOrRegister(new Application.Requests.Users.Commands.UserForLoginOrRegister("buibong2912", "29122002Az@"));
         
        }
    }
}
