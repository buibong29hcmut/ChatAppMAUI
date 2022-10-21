using ChatApp.Application.Cores.Queries;
using ChatApp.Application.Response.Messages;
using ChatApp.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Requests.Messages.Queries
{
    public class GetMesssageByConversationIdQuery: PaginationBaseParametter,IQuery<Result<PageList<MessageResponseByConversationId>>>
    {
        public Guid ConversationId { get; set; }
        public Guid UserId { get; set; }
       
    }

}
