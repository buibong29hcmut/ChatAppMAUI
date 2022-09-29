using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace ChatApp.API.AuthFillters
{
    public class AuthorizationUserIdFillter: IAsyncActionFilter
    {   
        

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userIdRequest = (Guid)context.ActionArguments["userId"];
            var userIdAuthorization = context.HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value;
            if (userIdRequest != new Guid(userIdAuthorization))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            await next();

        }
    }
}
