using ChatApp.Application.Cores.Commands;
using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Requests.Users.Commands;
using ChatApp.Share.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Commands.User.CreateOrUpdate
{
    public class UpdateProfileCommandHandler : ICommandHandler<UpdateProfileUserCommand, Result<Unit>>
    {
        private readonly IChatDbContext _db;
        public UpdateProfileCommandHandler(IChatDbContext db)
        {
            _db = db;
        }
        public async Task<Result<Unit>> Handle(UpdateProfileUserCommand request, CancellationToken cancellationToken)
        {
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
