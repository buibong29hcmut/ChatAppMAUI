using ChatApp.Application.Cores.Commands;
using ChatApp.Application.Interfaces.Services;
using ChatApp.Application.Models;
using ChatApp.Application.Requests.Users.Commands;
using ChatApp.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Commands.User.CreateOrUpdate
{
    public class LoginOrRegisterUserCommandHandler:ICommandHandler<UserForLoginOrRegisterCommand,Result<IdentityResult>>
    {
        private readonly IAuthenticateService _auth;
        public LoginOrRegisterUserCommandHandler(IAuthenticateService auth)
        {
            _auth = auth;
        }

        public async Task<Result<IdentityResult>> Handle(UserForLoginOrRegisterCommand request, CancellationToken cancellationToken)
        {
            var identityResult = await _auth.LoginOrRegister(request);
            return Result<IdentityResult>.Success(identityResult);
        }
    }
}
