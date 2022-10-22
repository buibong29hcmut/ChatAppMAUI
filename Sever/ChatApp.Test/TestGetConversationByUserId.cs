using ChatApp.Application.Cores.Queries;
using ChatApp.Application.Requests.Conversations;
using ChatApp.Application.Response.Conversations;
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
    public class TestGetConversationByUserId: BaseTest
    {
        [TestMethod]
        public async Task GetConversationByUserId()
        {
            Guid UserId = new Guid("e12da499-5c64-44bb-a581-7a304b312860");
            var services = serviceCollectionContainer.ServiceCollection();
            var queryBus = services.BuildServiceProvider().GetRequiredService<IQueryBus>();
            var allConversation =await queryBus.Send<Result<IReadOnlyCollection<BoxChatResponse>>>(new GetBoxChatByUserId(UserId, 0, 10));
            Assert.AreEqual(allConversation.Data.Count, 10);
        }
    }
}
