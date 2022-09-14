using ChatApp.Application.Cores.Commands;
using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Requests.Messages.Commands;
using ChatApp.Domain.Entities;
using ChatApp.Share.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var message = new Message(request.FromUserId,request.Content,request.SendTime,request.ConversationId);
            await _db.Messages.AddAsync(message);
            await _db.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
