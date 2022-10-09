using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Models
{
    public class UserProfile
    {
        public string Name { get; set; }
        public string UrlProfile { get; set; }
        public bool IsOnline { get; set; }
    }
}
