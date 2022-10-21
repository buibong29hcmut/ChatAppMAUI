using ChatApp.Application.Cores.Queries;
using ChatApp.Application.Response;
using ChatApp.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Requests.BoxChats
{
    public class GetConversationByTwoUserIdInConversation: IQuery<Result<GetConversationByTwoUserIdInConversation>>
    {
        public Guid UserId { get; set; }
        public Guid OtherUserId { get; set; }
    }
}
