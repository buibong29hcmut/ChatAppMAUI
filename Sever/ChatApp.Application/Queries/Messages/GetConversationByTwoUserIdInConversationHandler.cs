using ChatApp.Application.Cores.Queries;
using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Requests.Messages.Queries;
using ChatApp.Application.Response.Messages;
using ChatApp.Share.Wrappers;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly IChatDbContext _db;

        public GetConversationByTwoUserIdInConversationHandler(IDbFactory factory, IChatDbContext db)
        {
            _factory = factory;
            _db = db;   
        }

        public async Task<Result<ConversationResponseByTwoUserId>> Handle(GetConversationByTwoUserIdInConversation request, CancellationToken cancellationToken)
        {
            string query = "SELECT \"Id\" FROM public.\"Conversations\"\r\n" +
                           "WHERE (\"UserId\"= @userId\r\n" +
                           "AND \"OtherUserId\"=@otherUserId)\r\n" +
                           "OR (\"UserId\"=@otherUserId\r\n" +
                           "AND \"OtherUserId\"=@userId)\r\n" +
                           "LIMIT 1";
            bool CheckTwoUserInDb = (await _db.Users.AnyAsync(p => p.Id == request.UserId)) && (await _db.Users.AnyAsync(p => p.Id == request.OtherUserId));
            if (!CheckTwoUserInDb)
            {
                throw new Exception("Users aren't in database");
            }
            using (var connection= _factory.CreateConnection())
            {   
                Guid ConversationId = await connection.QueryFirstAsync<Guid>(query,new
                {
                    userId = request.UserId,
                    otherUserId = request.OtherUserId,
                });
                if (ConversationId.Equals(Guid.Empty))
                {
                    var conversationEntity = new Domain.Entities.Conversation(request.UserId, request.OtherUserId, DateTime.Now);
                    await _db.Conversations.AddAsync(conversationEntity);
                    await _db.SaveChangesAsync();
                    return Result<ConversationResponseByTwoUserId>.Success(new ConversationResponseByTwoUserId()
                    {
                        ConversationId = conversationEntity.Id,    
                    });
                }
                return Result<ConversationResponseByTwoUserId>.Success(new ConversationResponseByTwoUserId()
                {
                    ConversationId = ConversationId
                });

            }
        }
        
    }
}
