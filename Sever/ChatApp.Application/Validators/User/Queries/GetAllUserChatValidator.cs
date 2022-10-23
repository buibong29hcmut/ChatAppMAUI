using ChatApp.Application.Requests.Users.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Validators.User.Queries
{
    public class GetAllUserChatValidator: AbstractValidator<GetAllProfileUserQuery>
    {
        public GetAllUserChatValidator()
        {
            RuleFor(p=>p.UserId).NotEmpty();
            RuleFor(p => p.PageSize).Must(p => p > 0);
            RuleFor(p => p.PageNumber).Must(p => p > 0);
            
        }
    }
}
