using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Models
{
    public class AuthenticateModel
    {
        public string JwtToken { get; set; }
        public UserInfo Info { get; set; }
    }
    public class UserInfo
    {
        public  Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string UrlAvatar { get; set; }
    }
}
