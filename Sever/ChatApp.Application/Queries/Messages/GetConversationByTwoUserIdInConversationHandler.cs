using ChatApp.Application.Cores.Queries;
using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Requests.Messages.Queries;
using ChatApp.Application.Response.Messages;
using ChatApp.Share.Wrappers;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Queries.Messages
{
    public class GetConversationByTwoUserIdInConversationHandler : IQueryHandler<GetConversationByTwoUserIdInConversation, Result<ConversationResponseByTwoUserId>>
    {
        private readonly IDbFactory _factory;
        public GetConversationByTwoUserIdInConversationHandler(IDbFactory factory)
        {
            _factory = factory;
        }

        public async Task<Result<ConversationResponseByTwoUserId>> Handle(GetConversationByTwoUserIdInConversation request, CancellationToken cancellationToken)
        {
            string query = "SELECT \"Id\" FROM public.\"Conversations\"\r\n" +
                           "WHERE (\"UserId\"= @userId\r\n" +
                           "AND \"OtherUserId\"=@otherUserId)\r\n" +
                           "OR (\"UserId\"=@otherUserId\r\n" +
                           "AND \"OtherUserId\"=@userId)\r\n" +
                           "LIMIT 1";
            using (var connection= _factory.CreateConnection())
            {
                Guid ConversationId = await connection.QueryFirstAsync<Guid>(query,new
                {
                    userId = request.UserId,
                    otherUserId = request.OtherUserId,
                });
                if (ConversationId.Equals(Guid.Empty))
                {
                    return Result<ConversationResponseByTwoUserId>.Success(ConversationResponseByTwoUserId.Default);
                }
                return Result<ConversationResponseByTwoUserId>.Success(new ConversationResponseByTwoUserId()
                {
                    ConversationId = ConversationId
                });

            }
            return default;
        }
        
    }
}
