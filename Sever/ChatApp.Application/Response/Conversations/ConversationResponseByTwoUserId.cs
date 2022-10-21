using ChatApp.Application.Cores.Queries;
using ChatApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Response.Conversations
{
    public class ConversationResponseByTwoUserId
    {
        public Guid ConversationId { get; set; }
        public List<MessageModel> Messages { get; set; }
        public bool HasConversation => ConversationId.Equals(Guid.Empty);
     
    }
    public class MessageConversationResponseByTwoUserId : MessageModel
    {

    }
}
