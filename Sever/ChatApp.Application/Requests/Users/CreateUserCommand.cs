﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Requests.Users
{
    public class CreateUserCommand
    {
        UserForLoginOrRegister User { get; set; }
    }
}
