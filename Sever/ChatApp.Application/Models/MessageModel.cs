using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Models
{
    public class MessageModel
    {   
        public string Content { get; set; }
        public DateTimeOffset Created { get; set; }
        public Guid FromUserId { get; set; }
        public bool IsSeen { get; set; }
      
    }
}
