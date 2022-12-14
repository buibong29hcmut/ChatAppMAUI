using ChatApp.API.AuthFillters;
using ChatApp.Application.Cores.Commands;
using ChatApp.Application.Cores.Queries;
using ChatApp.Application.Requests.Conversations;
using ChatApp.Application.Requests.Messages.Queries;
using ChatApp.Application.Response.Conversations;
using ChatApp.Application.Response.Messages;
using ChatApp.Share.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Security.Claims;

namespace ChatApp.API.Controllers
{
    [Route("api/v1/user/{userId}")]
    public class ConversationController:BaseApiController
    {
        public ConversationController(ICommandBus _cmd, IQueryBus _query) : base(_cmd, _query)
        {

        }
   
        [HttpGet("conversation")]
        [ServiceFilter(typeof(AuthorizationUserIdFillter))]
        public async Task<IActionResult> GetConversationByUserId(Guid userId,[BindRequired] int CountConversation, [BindRequired] int RowFetch)
        {
            var result= await _query.Send<Result<IReadOnlyCollection<BoxChatResponse>>>(new GetBoxChatByUserId(userId, CountConversation, RowFetch));
            return Ok(result);
        }
        [HttpGet]
        [Route("conversation/{otherUserId}")]
        [ServiceFilter(typeof(AuthorizationUserIdFillter))]
        public async Task<IActionResult> GetConversationByTwoUserId(Guid userId, Guid otherUserId)
        {
            var result = await _query.Send<Result<ConversationResponseByTwoUserId>>(new GetConversationByTwoUserIdInConversation()
            {
                UserId = userId,
                OtherUserId = otherUserId,
            });
            return Ok(result);
        }



    }
}
