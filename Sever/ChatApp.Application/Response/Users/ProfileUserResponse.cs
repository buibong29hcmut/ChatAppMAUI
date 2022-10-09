using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Response.Users
{
    public class ProfileUserResponse
    {   public Guid  Id { get; set; }
        public string UserName { get; set; }
        public string UrlAvatar { get; set; }
        public string Name { get; set; }
      
    }
    public class ProfileUserResponseWithOperation:ProfileUserResponse
    {
        public bool IsOnline { get; set; } = false;
    }
}
