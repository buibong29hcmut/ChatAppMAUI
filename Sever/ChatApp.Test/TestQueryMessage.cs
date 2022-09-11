using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Response;
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
            string query = "\tSELECT c.\"Id\" as \"ConversationId\", \"UserId\", \"OtherUserId\",\r\n\t \"LastMessageId\",m.\"Content\", m.\"SendTime\", m.\"Read\"\r\n\tFROM public.\"Conversations\" c\r\n    INNER JOIN \"Messages\" m \r\n\tON c.\"LastMessageId\"=m.\"Id\"\r\n\tAND (\"UserId\"=@userId OR \"OtherUserId\"=@userId )\r\n\tORDER BY m.\"SendTime\" desc";
            var _factory = serviceContainer.ServiceCollection().BuildServiceProvider().GetRequiredService<IDbFactory>();
            using (var db = _factory.CreateConnection())
            {
                IEnumerable<BoxChatRawQuery> boxChatRaws = await db.QueryAsync<BoxChatRawQuery>(query, new {UserId=new Guid("91ccf996-29ed-4dce-b5e4-d555e64daf44") });
                Assert.AreEqual(boxChatRaws.Count(), 100);
            }
            

        }
    }
}
