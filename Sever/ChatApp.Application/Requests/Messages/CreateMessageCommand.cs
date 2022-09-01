using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Requests.Messages
{
    public class CreateMessageCommand
    {
        public Guid FromUSerId { get; set; }
        public Guid ToUSerID { get; set; }
        public string Content { get; set; }
        public DateTime SendTime { get; set; }= DateTime.Now;   

    }
}
