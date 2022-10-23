using ChatApp.Application.Cores.Commands;
using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Requests.Messages.Commands;
using ChatApp.Domain.Entities;
using ChatApp.Share.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Commands.Messages
{
    public class CreateMessageCommandHandler : ICommandHandler<CreateMessageCommand, Result<Unit>>
    {
        private readonly IChatDbContext _db;
        public CreateMessageCommandHandler(IChatDbContext db)
        {
            _db = db;   
        }


        public async Task<Result<Unit>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var ConversationIsValid = await CheckConversationIsValid(request.FromUserId, request.ToUserId, request.ConversationId);
            if (!ConversationIsValid)
                return Result<Unit>.Fail("Conversation isn't valid");
            var message = new Message(request.FromUserId,request.Content, request.SendTime,request.ConversationId);       
            await _db.Messages.AddAsync(message);
            var conversation = await _db.Conversations.Where(p => p.Id == request.ConversationId).FirstOrDefaultAsync();
            conversation.SetLastMessage(message.Id);
            await _db.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value);
        }


        private async   Task<bool> CheckConversationIsValid(Guid FromUserId, Guid OtherUserId, Guid ConversationId)
        {
            var checkConversationIsValid = await _db.Conversations.AnyAsync(FuncCheckConcersationISValid(FromUserId, OtherUserId, ConversationId));
            if (checkConversationIsValid)
                return true;
            return false;
        }
        private Expression<Func<Conversation,bool>> FuncCheckConcersationISValid(Guid FromUserId, Guid OtherUserId,Guid ConversationId)
        {
            return p=>  (p.UserId==OtherUserId&& p.OtherUserId==FromUserId && p.Id == ConversationId)
                     || (p.UserId == FromUserId && p.OtherUserId == OtherUserId && p.Id == ConversationId);
        }
    }
}
