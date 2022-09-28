using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public LoginModel(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }
    }
}
