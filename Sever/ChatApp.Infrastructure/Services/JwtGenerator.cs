using ChatApp.Application.Interfaces.Services;
using ChatApp.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace ChatApp.Infrastructure.Services
{
    public class JwtGenerator: IJwtGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly IDateTimeProvider _datetimeProvider;
        public JwtGenerator(IConfiguration configuration, IDateTimeProvider datetimeProvider)
        {
            _configuration = configuration;
            _datetimeProvider = datetimeProvider;   
        }
        public string GenerateToken(Guid userId,string UserName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("SecretKey").Value);
         

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Name, UserName),
                }),
                Expires = _datetimeProvider.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Issuer= "JWTAuthenticationServer",
                Audience= "JWTServicePostmanClient",
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
