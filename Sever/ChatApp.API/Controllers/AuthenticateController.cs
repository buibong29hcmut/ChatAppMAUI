using ChatApp.Application.Cores.Commands;
using ChatApp.Application.Cores.Queries;
using ChatApp.Application.Models;
using ChatApp.Application.Requests.Users.Commands;
using ChatApp.Domain.Entities;
using ChatApp.Share.Wrappers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ChatApp.API.Controllers
{
    [Route("api/v1/auth")]   
    public class AuthenticateController:BaseApiController
    {
        public AuthenticateController(ICommandBus cmd, IQueryBus query) : base(cmd, query)
        {

        }
        const string callbackScheme = "chatmaui";

        [HttpGet("{scheme}")] 
        public async Task Get([FromRoute] string scheme)
        {
            var auth = await Request.HttpContext.AuthenticateAsync(scheme);

            if (!auth.Succeeded
                || auth?.Principal == null
                || !auth.Principal.Identities.Any(id => id.IsAuthenticated)
                || string.IsNullOrEmpty(auth.Properties.GetTokenValue("access_token")))
            {
                // Not authenticated, challenge
                await Request.HttpContext.ChallengeAsync(scheme);
            }
            else
            {
                var claims = auth.Principal.Identities.FirstOrDefault()?.Claims;
                string email = claims?.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;
                string givenName = claims?.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.GivenName)?.Value;
                string surName = claims?.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Surname)?.Value;
                string picture = claims?.FirstOrDefault(c => c.Type == "picture")?.Value;

                UserForLoginGoogleCommand User = new UserForLoginGoogleCommand()
                {
                    Email = email,
                    Name = givenName + " " + surName,
                    UrlAvatar= picture
                };
               var result=await  _command.Send<Result<UserIdentity>>(User);
                var qs = new Dictionary<string, string>
                {
                    { "access_token", result.Data.JwtToken },
                    { "Id", result.Data.Info.Id.ToString() }
                };
              
                var url = callbackScheme + "://#" + string.Join(
                    "&",
                    qs.Where(kvp => !string.IsNullOrEmpty(kvp.Value) && kvp.Value != "-1")
                    .Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}"));

                Request.HttpContext.Response.Redirect(url);
            }
        }
        [HttpPost("log_in")]
        public async Task<IActionResult> LoginUser([FromBody] UserForLoginCommand cmd)
        {
            var result = await _command.Send<Result<UserIdentity>>(cmd);
            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegisterCommand cmd)
        {
            var result = await _command.Send<Result<UserIdentity>>(cmd);
            return Ok(result);
        }
    }
}
