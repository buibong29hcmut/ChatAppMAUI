using ChatApp.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Entities
{
    public class User : Entity
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string UrlAvater { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public byte[] Salt { get; private set; }

    }
}
