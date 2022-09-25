using ChatApp.Application.Cores.Queries;
using ChatApp.Application.Requests.Messages.Queries;
using ChatApp.Application.Response.Messages;
using ChatApp.Share.Wrappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Test
{
    [TestClass]   
    public class TestGetMessageByConversationId
    {   private ServiceCollectionContainer serviceCollectionContainer = new ServiceCollectionContainer();   
        [TestMethod]   
        public async Task GetMessageByConversationId()
        {
            var services = serviceCollectionContainer.ServiceCollection();
            var queryBus = services.BuildServiceProvider().GetRequiredService<IQueryBus>();
            var message = await queryBus.
                Send<Result<PageList<MessageResponseByConversationId>>>(new GetMesssageByConversationIdQuery()
                {
                    ConversationId= new Guid("04f32922-ab5d-4e87-9b0c-1e5c1c23783e"),
                    PageNumber=1,
                    PageSize=10,
                });
        }
    }
}
