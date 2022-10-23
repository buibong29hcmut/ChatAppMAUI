using ChatApp.Application.Requests.Users.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Validators.User.Queries
{
    public class GetProfileUserQueryValidator: AbstractValidator<GetProfileUserQuery>
    {
        public GetProfileUserQueryValidator()
        {
            RuleFor(p => p.UserId).NotEmpty();
           
        }
    }
}
