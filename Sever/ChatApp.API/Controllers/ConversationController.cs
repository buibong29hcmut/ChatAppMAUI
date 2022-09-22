using ChatApp.Application.Cores.Commands;
using ChatApp.Application.Cores.Queries;
using ChatApp.Application.Requests.Messages.Queries;
using ChatApp.Application.Response.Messages;
using ChatApp.Share.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    [Route("api/v1/conversation")]   
    public class ConversationController:BaseApiController
    {
        public ConversationController(ICommandBus _cmd, IQueryBus _query) : base(_cmd, _query)
        {

        }
    
     
    }
}
