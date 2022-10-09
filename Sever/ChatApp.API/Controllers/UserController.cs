using Azure.Core.Pipeline;
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
    [Authorize]     
    public class UserController:BaseApiController
    {
        private readonly IUploadFileToAzureBlobService _uploader;
        public UserController(ICommandBus command, IQueryBus queryBus, IUploadFileToAzureBlobService uploader) : base(command, queryBus)
        {
            _uploader = uploader;
        }
      
        [HttpGet]
        public async Task<IActionResult> GetAllUser(int pageSize,int pageNumber)
        {
            var result = await _query.Send<Result<PageList<ProfileUserResponseWithOperation>>>(new GetAllProfileUserQuery()
            {
                UserId= new Guid(UserId),
                PageNumber= pageNumber,
                PageSize= pageSize
            });
            return Ok(result);

        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetProfileUser(Guid userId)
        {
            var result = await _query.Send<Result<ProfileUserResponseWithOperation>>(new GetProfileUserQuery()
            {
                UserId = userId
            });
            return Ok(result);
        }
        [HttpPost("upload_avt")]
        public async Task<IActionResult> UploadAvatar([FromForm] IFormFile file)
        {
            var result =await _uploader.UploadFile(file);
            return Ok(new
            {
                Url = result
            });
        }
       
    }
}
