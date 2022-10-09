using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Models
{
    public class UserModel:UserProfileModel
    {
        public bool IsOnline { get; set; }

    }
    public class UserProfileModel
    { 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string UrlAvatar { get; set; }

    }
}
