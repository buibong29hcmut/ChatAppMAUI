using ChatApp.Application.Cores.Commands;
using ChatApp.Application.Cores.Queries;
using ChatApp.Application.Models;
using ChatApp.Application.Requests.Users.Commands;
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

        [HttpGet("{scheme}")] // eg: Microsoft, Facebook, Apple, etc
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
                var email = string.Empty;
                email = claims?.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;

                var qs = new Dictionary<string, string>
                {
                    { "access_token", auth.Properties.GetTokenValue("access_token") },
                    { "refresh_token", auth.Properties.GetTokenValue("refresh_token") ?? string.Empty },
                    { "expires", (auth.Properties.ExpiresUtc?.ToUnixTimeSeconds() ?? -1).ToString() },
                    { "email", email }
                };

                // Build the result url
                var url = callbackScheme + "://#" + string.Join(
                    "&",
                    qs.Where(kvp => !string.IsNullOrEmpty(kvp.Value) && kvp.Value != "-1")
                    .Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}"));

                Request.HttpContext.Response.Redirect(url);
            }
        }
        [HttpPost]
        public async Task<IActionResult> LoginOrRegister([FromBody] UserForLoginOrRegisterCommand cmd)
        {
            var result = await _command.Send<Result<IdentityResult>>(cmd);
            return Ok(result);
        }
    }
}
