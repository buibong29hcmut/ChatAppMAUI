using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Requests.Users.Commands
{
    public class UpdateProfileUserCommand
    {
        public string UrlProfile { get; set; }
        public string Name { get; set; }
    }
}
