using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Response.Messages
{
    public class MessageResponseByConversationId
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTimeOffset SendTime { get; set; }
        public Guid FromUserId { get; set; }
    }
}
