using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Interfaces.Services;
using ChatApp.Application.Models;
using ChatApp.Application.Requests.Users.Commands;
using ChatApp.Application.Specifications.Contracts;
using ChatApp.Domain.Entities;
using ChatApp.Share.Wrappers;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Services
{
    public class AuthenticateService:IAuthenticateService
    {
        private readonly IDbFactory _dbFactory;
        private readonly IPasswordHasher _hasher;
        private readonly IJwtGenerator _jwtGenerator;
        public AuthenticateService(
            IDbFactory dbFactory,
            IPasswordHasher hasher
            ,IJwtGenerator jwtGenerator
           )
        {
            _dbFactory = dbFactory;
            _hasher = hasher;
            _jwtGenerator = jwtGenerator;
        }
        public async  Task<Result<UserIdentity>> LoginOrRegister(UserForLoginOrRegister userInfo)
        {
            using (var connection = _dbFactory.CreateConnection())
            {
                string queryUser = "SELECT \"Id\", \"UserName\", \"UrlAvatar\",\"Password\",\"Salt\"" +","+
                "\"Password\" FROM public.\"Users\" WHERE \"UserName\"=@UserName LIMIT 1";
                var user= await connection.QueryFirstOrDefaultAsync<User>(queryUser,new { UserName = userInfo.UserName});
                if (user != null)
                {
                    bool checkPassword = _hasher.CheckPassWord(userInfo.Password, user.Password, user.Salt);
                    if (checkPassword)
                    {
                        string token = _jwtGenerator.GenerateToken(user.Id, user.UserName);
                        return IdentityResult.Success(new UserIdentity()
                        {
                            JwtToken= token,
                            Info= new UserInfo()
                            {
                                UrlAvatar=user.UrlAvatar,
                                UserName=user.UserName,
                                Name=user.Name,
                                Id=user.Id,
                            }
                        });

                    }
                    return IdentityResult.Fail(new List<string>()
                    {
                        "Password is not correct"
                    });
                }
                var hashPassWordResult= _hasher.HashWithSHA256Algo(userInfo.Password);
                User registerUser = User.CreateUser(userInfo.UserName, hashPassWordResult.PasswordHash, hashPassWordResult.Salt);
              int result=
                    await connection
                    .ExecuteAsync($"INSERT INTO public.\"Users\"(\r\n\t \"Id\", \"UserName\", \"Password\",\"Salt\")\r\n\tVALUES (@Id,@UserName,@Password,@Salt)", 
                    new {
                        Id=registerUser.Id,
                        UserName = registerUser.UserName,
                        Password = registerUser.Password,
                        Salt= registerUser.Salt
                       });
                string tokenForRegister= _jwtGenerator.GenerateToken(registerUser.Id, registerUser.UserName);
                return IdentityResult.Success(new UserIdentity()
                {
                    JwtToken = tokenForRegister,
                    Info = new UserInfo()
                    {
                        UrlAvatar = user.UrlAvatar,
                        UserName = user.UserName,
                        Name = user.Name,
                        Id = user.Id,
                    }
                });

            }
        }
        
    }
}
