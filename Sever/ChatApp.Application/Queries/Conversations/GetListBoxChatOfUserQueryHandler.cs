using ChatApp.Application.Cores.Queries;
using ChatApp.Application.Interfaces.DAL;
using ChatApp.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ChatApp.Application.Interfaces.Services;
using System.Collections.ObjectModel;
using ChatApp.Application.Response.Conversations;
using ChatApp.Application.Requests.Conversations;

namespace ChatApp.Application.Queries.Conversations
{
    public class GetListBoxChatOfUserQueryHandler:IQueryHandler<GetBoxChatByUserId,Result<IReadOnlyCollection<BoxChatResponse>>>
    {
        private readonly IDbFactory _factory;
        private readonly IUserOperation _operation;
        public GetListBoxChatOfUserQueryHandler(IDbFactory factory,IUserOperation operation)
        {
            _factory = factory;
            _operation = operation; 
        }

        public async Task<Result<IReadOnlyCollection<BoxChatResponse>>> Handle(GetBoxChatByUserId request, CancellationToken cancellationToken)
        {
            string query = "SELECT c.\"Id\" as \"ConversationId\", \"UserId\", \"OtherUserId\",\r\n \"LastMessageId\",m.\"Content\", m.\"SendTime\", m.\"Read\"\r\n\tFROM public.\"Conversations\" c\r\n    INNER JOIN \"Messages\" m \r\n\tON c.\"LastMessageId\"=m.\"Id\"\r\n\tAND (\"UserId\"=@userId \r\n\tOR \"OtherUserId\"=@userId)\r\n\tORDER BY m.\"SendTime\" desc\r\n\tLIMIT @rowcount\r\n\tOFFSET @rowConversation\r\n\t"; ;
            List<BoxChatResponse> result = new List<BoxChatResponse>();

            using (var db = _factory.CreateConnection())
            {
                var parametterQueryConversation = new
                {
                    UserId = request.UserId,
                    OtherUserId = request.UserId,
                    rowConversation = request.CountConversation,
                    rowcount = request.RowFetch
                };
                IEnumerable<BoxChatRawQuery> boxChatRaws = await db.QueryAsync<BoxChatRawQuery>(query, parametterQueryConversation);
                foreach(var boxChatRaw in boxChatRaws)
                {  
                    Guid UserQueryProfile = boxChatRaw.UserId!=request.UserId? boxChatRaw.UserId : boxChatRaw.OtherUserId;
                    string queryProfile = "SELECT \"Id\",  \"Name\",\"UserName\",\"UrlAvatar\" as \"UrlProfile\" FROM public.\"Users\"\r\n\tWHERE \"Id\"= @userId\r\n\tLIMIT 1";
                    UserProfileByConversation userByConersation = await db.QueryFirstOrDefaultAsync<UserProfileByConversation>(queryProfile, new {userId= UserQueryProfile });

                    BoxChatResponse boxChatResponse = new BoxChatResponse()
                    {
                        ConversationId = boxChatRaw.ConversationId,
                        Message = boxChatRaw.Content,
                        SeenMessage = boxChatRaw.Read,
                        TimeMessage = boxChatRaw.SendTime,
                        User = new UserBoxChatResponse()
                        {   
                            Id = userByConersation.Id,
                            UrlProfile = userByConersation.UrlProfile,
                            Name = userByConersation.Name,
                            IsOnline = await _operation.IsUserOnline(userByConersation.Id.ToString())
                        }
                        
                    };
                    result.Add(boxChatResponse);
                }
            }
            return Result<IReadOnlyCollection<BoxChatResponse>>.Success(result);
        }
    }
}
