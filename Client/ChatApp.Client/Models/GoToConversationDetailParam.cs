using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Models
{
    public class GoToConversationDetailParam
    {
        public Guid ConversationId { get; set; }
        public Guid OtherUserId { get; set; }
    }
}
