using ChatApp.Application.Cores.Commands;
using ChatApp.Application.Cores.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatApp.API.Controllers
{
    [ApiController]
    public class BaseApiController:ControllerBase
    {
        protected readonly ICommandBus _command;
        protected readonly IQueryBus _query;
        public BaseApiController(ICommandBus command, IQueryBus query)
        {
            _command = command;
            _query = query;
        }
        public string UserId
        {
            get
            {
                return HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value;
            }
        }
    }
}
