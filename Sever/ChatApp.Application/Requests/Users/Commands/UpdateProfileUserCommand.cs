using ChatApp.Application.Cores.Commands;
using ChatApp.Share.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Requests.Users.Commands
{
    public class UpdateProfileUserCommand:ICommand<Result<Unit>>
    {
        public string UrlProfile { get; set; }
        public string Name { get; set; }
    }
}
