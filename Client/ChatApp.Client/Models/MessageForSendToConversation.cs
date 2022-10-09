using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Models
{
    public class MessageForSendConversation
    {
        public Guid ConversationId { get; set; }
        public string Content { get; set; }
        public DateTime SendTime { get; set; } = DateTime.Now;
    }
    public class UserInfoForSendMessageConversation
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public string UrlProfile { get; set; }
    }

}
