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

namespace ChatApp.Application.Queries.Conversations
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

            var conversationId = await _db.Conversations.Where(p => p.UserId == request.UserId && p.OtherUserId == request.OtherUserId
                                                                     || p.UserId == request.OtherUserId && p.OtherUserId == request.UserId)
                                                    .Select(p => p.Id)
                                                    .FirstOrDefaultAsync();

            if (conversationId.Equals(Guid.Empty))
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
                ConversationId = conversationId
            });


        }

    }
}
