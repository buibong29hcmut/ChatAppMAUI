using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Requests.Messages.Queries
{
    public class GetMesssageByConversationIdQuery
    {
        public Guid ConversationId { get; set; }
       
    }
}
