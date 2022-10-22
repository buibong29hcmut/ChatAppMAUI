using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Requests.Conversations;
using ChatApp.Application.Response.Conversations;
using ChatApp.Infrastructure.Contexts;
using Dapper;
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
        ServiceCollectionContainer serviceContainer = new ServiceCollectionContainer();

      
        [TestMethod]
        public async Task GetBoxChatRawQuery()
        {
            string query = "SELECT c.\"Id\" as \"ConversationId\", \"UserId\", \"OtherUserId\",\r\n    \"LastMessageId\",m.\"Content\", m.\"SendTime\", m.\"Read\"\r\n\tFROM public.\"Conversations\" c\r\n    INNER JOIN \"Messages\" m \r\n\tON c.\"LastMessageId\"=m.\"Id\"\r\n\tAND (\"UserId\"=@userId \r\n\tOR \"OtherUserId\"=@userId)\r\n\tORDER BY m.\"SendTime\" desc\r\n\tLIMIT @rowcount\r\n\tOFFSET @rowConversation\r\n\t";
            var _factory = serviceContainer.ServiceCollection().BuildServiceProvider().GetRequiredService<IDbFactory>();
            GetBoxChatByUserId request = new GetBoxChatByUserId(new Guid("e12da499-5c64-44bb-a581-7a304b312860"), 0, 10);
            var parametterQueryConversation = new
            {
                UserId = request.UserId,
                OtherUserId = request.UserId,
                rowConversation = request.CountConversation,
                rowcount = request.RowFetch
            };
            using (var db = _factory.CreateConnection())
            {
                IEnumerable<BoxChatRawQuery> boxChatRaws = await db.QueryAsync<BoxChatRawQuery>(query, parametterQueryConversation );
                Assert.AreEqual(boxChatRaws.Count()>0, true);
            }
            

        }
    }
}
