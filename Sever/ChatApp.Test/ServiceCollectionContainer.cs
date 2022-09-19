using ChatApp.Application;
using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Interfaces.Services;
using ChatApp.Infrastructure;
using ChatApp.Infrastructure.Contexts;
using ChatApp.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Test
{
    public class ServiceCollectionContainer
    {
        public  ServiceCollection ServiceCollection()
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
               .AddEnvironmentVariables()
               .Build();

            ServiceCollection service = new ServiceCollection();
            service.AddSingleton(configuration);
            service.AddInfrastructure(configuration).AddApplication();
           
            return service;
        }
    }
}
