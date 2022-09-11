using ChatApp.Application.Cores.Queries;
using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Requests.BoxChats;
using ChatApp.Application.Response;
using ChatApp.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace ChatApp.Application.Queries.BoxChats
{
    public class GetBoxChatOfUser:IQueryHandler<GetBoxChatByUserId,Result<IReadOnlyCollection<BoxChatResponse>>>
    {
        private readonly IDbFactory _factory;
        public GetBoxChatOfUser(IDbFactory factory)
        {
            _factory = factory;
        }

        public async Task<Result<IReadOnlyCollection<BoxChatResponse>>> Handle(GetBoxChatByUserId request, CancellationToken cancellationToken)
        {
            string query = "\tSELECT c.\"Id\" as \"ConversationId\", \"UserId\", \"OtherUserId\",\r\n\t \"LastMessageId\",m.\"Content\", m.\"SendTime\", m.\"Read\"\r\n\tFROM public.\"Conversations\" c\r\n    INNER JOIN \"Messages\" m \r\n\tON c.\"LastMessageId\"=m.\"Id\"\r\n\tAND (\"UserId\"=@userId OR \"OtherUserId\"=@userId )\r\n\tORDER BY m.\"SendTime\" desc";
            using (var db = _factory.CreateConnection())
            {
                IEnumerable<BoxChatRawQuery> boxChatRaws = await db.QueryAsync<BoxChatRawQuery>(query, new { UserId = request.UserId });
                foreach(var boxChatRaw in boxChatRaws)
                {
                    Guid UserQueryProfile = Guid.Empty;
                    if (boxChatRaw.UserId != request.UserId)
                    {
                        UserQueryProfile= boxChatRaw.UserId;
                    }
                    else
                    {
                        UserQueryProfile = boxChatRaw.OtherUserId;
                    }
                    UserProfileByConversation userByConersation = await db.QueryFirstOrDefaultAsync<UserProfileByConversation>(query);
                    BoxChatResponse boxChatResponse = new BoxChatResponse()
                    {
                        ConversationId = boxChatRaw.ConversationId,
                        Message = boxChatRaw.Message,
                        SeenMessage = boxChatRaw.Read,
                        TimeMessage= boxChatRaw.SendTime,
                        UrlProfile=userByConersation.UrlProfile,
                        Name=userByConersation.UserName,

                    };
                }
            }
            return null;
        }
    }
}
