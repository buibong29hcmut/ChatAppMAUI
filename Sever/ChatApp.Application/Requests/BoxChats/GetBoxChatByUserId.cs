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
    public class GetBoxChatByUserId:IQuery<Result<IReadOnlyCollection<BoxChatResponse>>>
    {
        public Guid UserId { get; private set; }
        public GetBoxChatByUserId(Guid userId)
        {
            UserId = userId;
        }   
    }
}
