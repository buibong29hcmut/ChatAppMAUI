using ChatApp.Application.Cores.Commands;
using ChatApp.Application.Cores.Queries;
using ChatApp.Application.Requests.Messages.Queries;
using ChatApp.Application.Response.Messages;
using ChatApp.Share.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    [Route("api/v1/conversation/{conversationId}/messages")]
    public class MessageController:BaseApiController
    {
        public MessageController(ICommandBus _cmd,IQueryBus _query) : base(_cmd, _query)
        {

        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMessageByConversation(Guid conversationId,int pageSize, int PageNumber)
        {
            GetMesssageByConversationIdQuery param = new GetMesssageByConversationIdQuery()
            {
                ConversationId = conversationId,
                PageSize = pageSize,
                PageNumber = PageNumber,
                UserId=new Guid(UserId)
            };
            var result = await _query.Send<Result<PageList<MessageResponseByConversationId>>>(param);
            return Ok(result);

        }
     
    }
}
