using ChatApp.Application.Requests.Messages.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Validators.Message.Queries
{
    public class GetMesssageByConversationIdQueryValidator : AbstractValidator<GetMesssageByConversationIdQuery>
    {
        public GetMesssageByConversationIdQueryValidator()
        {
            RuleFor(p => p.ConversationId).NotEmpty();
            RuleFor(p => p.PageNumber).Must(p => p > 0);
            RuleFor(p => p.PageSize).Must(p => p > 0);
            RuleFor(p => p.UserId).NotEmpty();
        }
    }
}
