using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace ChatApp.API.AuthFillters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizationUserIdFillter : AuthorizeAttribute,IAuthorizationFilter
    {   private string UserId { get; set; }
        public AuthorizationUserIdFillter(string userId)
        {
            UserId = userId;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string userId=context.HttpContext.User.Claims.FirstOrDefault(p=>p.Type==ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId)|| UserId.Equals(userId))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
