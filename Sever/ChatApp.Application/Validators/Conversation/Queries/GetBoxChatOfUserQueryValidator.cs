using ChatApp.Application.Queries.Conversations;
using ChatApp.Application.Requests.Conversations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Validators.Conversation.Queries
{
    public class GetBoxChatOfUserQueryValidator: AbstractValidator<GetBoxChatByUserId>
    {
        public GetBoxChatOfUserQueryValidator()
        {
            RuleFor(p => p.RowFetch).NotEmpty();
            RuleFor(p => p.CountConversation).NotEmpty();
            RuleFor(p => p.UserId).NotEmpty();
           
        }
    }
}
