﻿using ChatApp.Application.Cores.Commands;
using ChatApp.Application.Models;
using ChatApp.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Requests.Users.Commands
{
    public class UserForLoginCommand :ICommand<Result<UserIdentity>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserForLoginCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
