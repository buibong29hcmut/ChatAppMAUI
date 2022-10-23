using ChatApp.Application.Requests.Messages.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Validators.Message.Commands
{
    public  class CreateMessageCommandValidator: AbstractValidator<CreateMessageCommand>
    {
        public CreateMessageCommandValidator()
        {
            RuleFor(p => p.Content).NotEmpty();
            RuleFor(p=>p.FromUserId).NotEmpty();
            RuleFor(p => p.ToUserId).NotEmpty();
            RuleFor(p => p.ConversationId).NotEmpty();
          

        }
    }
}
