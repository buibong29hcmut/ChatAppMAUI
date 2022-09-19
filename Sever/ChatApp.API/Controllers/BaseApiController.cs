using ChatApp.Application.Cores.Commands;
using ChatApp.Application.Cores.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    [ApiController]
    public class BaseApiController:ControllerBase
    {
        protected readonly ICommandBus _command;
        protected readonly IQueryBus _query;
      
    }
}
