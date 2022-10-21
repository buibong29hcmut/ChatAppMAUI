using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Models
{
    public class ConversationModel
    {
        public Guid ConversationId { get; set; }
        public bool HasConversation { get; set; }
    }
}
