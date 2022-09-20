using ChatApp.Application.Commands.User.CreateOrUpdate;
using ChatApp.Application.Cores.Commands;
using ChatApp.Application.Cores.Queries;
using ChatApp.Application.Interfaces.Services;
using ChatApp.Application.Models;
using ChatApp.Application.Requests.Users.Commands;
using ChatApp.Application.Requests.Users.Queries;
using ChatApp.Application.Response.Users;
using ChatApp.Domain.Entities;
using ChatApp.Share.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatApp.API.Controllers
{
    [Route("api/v1/user/")]   
    public class UserController:BaseApiController
    {
        private readonly IUploadFileToAzureBlobService _uploader;
        public UserController(ICommandBus command, IQueryBus queryBus, IUploadFileToAzureBlobService uploader) : base(command, queryBus)
        {
            _uploader = uploader;
        }
        public string UserId
        {
            get
            {
                return HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await _query.Send<Result<PageList<ProfileUserResponseWithOperation>>>(new GetAllProfileUserQuery()
            {
                UserId= new Guid("e12da499-5c64-44bb-a581-7a304b312860"),
                PageNumber=1,
                PageSize=10
            });
            return Ok(result);

        }
        [HttpPost]
        public async Task<IActionResult> LoginOrRegister([FromBody] UserForLoginOrRegisterCommand cmd)
        {
            var result= await  _command.Send<Result<IdentityResult>>(cmd);
            return Ok(result);
        }
        [HttpPost("upload_avt")]
        public async Task<IActionResult> UploadAvatar([FromForm] IFormFile file)
        {
            var result = _uploader.UploadFile(file);
            return Ok(new
            {
                Url = result
            });
        }
       
    }
}
