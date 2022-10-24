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
    public class LoginOrRegisterUserCommandHandler:ICommandHandler<UserForLoginCommand, Result<UserIdentity>>,
                                                   ICommandHandler<UserForRegisterCommand,Result<UserIdentity>>,
                                                  ICommandHandler<UserForLoginGoogleCommand, Result<UserIdentity>>

    {
        private readonly IAuthenticateService _auth;
        public LoginOrRegisterUserCommandHandler(IAuthenticateService auth)
        {
            _auth = auth;
        }

        public async Task<Result<UserIdentity>> Handle(UserForLoginCommand request, CancellationToken cancellationToken)
        {
            var identityResult = await _auth.LoginAsync(request);
            return identityResult;
        }

        public async Task<Result<UserIdentity>> Handle(UserForRegisterCommand request, CancellationToken cancellationToken)
        {
            return await _auth.RegisterAsync(request);
        }

        public async Task<Result<UserIdentity>> Handle(UserForLoginGoogleCommand request, CancellationToken cancellationToken)
        {
            return await _auth.LogInGoogleAsync(request);
        }
    }
}
