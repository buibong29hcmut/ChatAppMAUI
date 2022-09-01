using ChatApp.Infrastructure.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Test
{
    [TestClass]  
    public class TestQueryMessage
    {
        [TestMethod]
        public void GetListLatestMessage()
        {
            ServiceCollectionContainer serviceContainer= new ServiceCollectionContainer();
            var service = serviceContainer.ServiceCollection();
            var db = service.BuildServiceProvider().CreateAsyncScope().ServiceProvider.GetRequiredService<ChatDbContext>();
         
                                   
                                  

        }
    }
}
