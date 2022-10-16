using ChatApp.Application.Cores.Queries;
using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Requests.Messages.Queries;
using ChatApp.Application.Response.Messages;
using ChatApp.Domain.Entities;
using ChatApp.Share.Wrappers;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Queries.Messages
{
    public class GetMessageByConversationHandler:IQueryHandler<GetMesssageByConversationIdQuery,Result<PageList<MessageResponseByConversationId>>>
    {
        private readonly IDbFactory _factory;
        public GetMessageByConversationHandler(IDbFactory factory)
        {
            _factory = factory;
        }
        public async Task<Result<PageList<MessageResponseByConversationId>>> Handle(GetMesssageByConversationIdQuery para,CancellationToken cancellationToken)
        {
            string totalMessageQuery = "SELECT COUNT(\"Id\") FROM public.\"Messages\"\r\n\twhere \"ConversationId\"=@conversationId";
            using(var connection = _factory.CreateConnection())
            {   
                int totalCount = connection.QueryFirst<int>(totalMessageQuery, new { conversationId = para.ConversationId });
                string queryMessage = "SELECT \"Id\", \"FromUserId\", \"Content\", \"Read\", \"SendTime\" FROM public.\"Messages\"  \r\nwhere \"ConversationId\"=@conversationId\r\nORDER BY \"SendTime\"\r\n desc\r\n LIMIT @pageSize\r\n OFFSET  (@pageNumber-1)*@pageSize";
                IEnumerable<MessageResponseByConversationId> messageResult =
                   await connection.QueryAsync<MessageResponseByConversationId>(queryMessage, new
                    {
                        userId= para.UserId,
                        ConversationId = para.ConversationId,
                        pageSize=para.PageSize,
                        pageNumber=para.PageNumber,
                    });
                PageList<MessageResponseByConversationId> result = new PageList<MessageResponseByConversationId>(messageResult.ToList(), totalCount, para.PageNumber, para.PageSize);
                return Result<PageList<MessageResponseByConversationId>>.Success(result);
            }
        }
    }
}
