using ChatApp.Application.Requests.Messages.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Validators.Conversation.Queries
{
    public class GetConversationByTwoUserIdInConversationValidator:AbstractValidator<GetConversationByTwoUserIdInConversation>
    {
        public GetConversationByTwoUserIdInConversationValidator()
        {
            RuleFor(p => p.UserId).NotEmpty();
            RuleFor(p => p.OtherUserId).NotEmpty();
           
        }
    }
}
