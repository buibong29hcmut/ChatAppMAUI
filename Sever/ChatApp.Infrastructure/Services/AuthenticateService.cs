using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Interfaces.Services;
using ChatApp.Application.Models;
using ChatApp.Application.Requests.Users.Commands;
using ChatApp.Domain.Entities;
using ChatApp.Share.Wrappers;
using Dapper;
using Microsoft.EntityFrameworkCore;
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
        private readonly IChatDbContext _db;
        public AuthenticateService(
            IDbFactory dbFactory,
            IPasswordHasher hasher
           ,IJwtGenerator jwtGenerator,
            IChatDbContext db
           )
        {
            _dbFactory = dbFactory;
            _hasher = hasher;
            _jwtGenerator = jwtGenerator;
            _db = db;
        }
        public async  Task<Result<UserIdentity>> LoginAsync(UserForLoginCommand userInfo)
        {
            using (var connection = _dbFactory.CreateConnection())
            {
                string queryUser = "SELECT \"Id\", \"UserName\",\"Name\", \"UrlAvatar\",\"Password\",\"Salt\"" +","+
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
                                Id=user.Id
                            }
                        });

                    }
                    return IdentityResult.Fail(new List<string>()
                    {
                        "Password is not correct"
                    });
                }
                return Result<UserIdentity>.Fail("UserName Or Password isn't correct");

            }
            
        }
        public async Task<Result<UserIdentity>> LogInGoogleAsync(UserForLoginGoogleCommand userInfo)
        {
            var user = await _db.Users.Where(p => p.Email == userInfo.Email).FirstOrDefaultAsync();
            if (user != null)
            {
                string token = _jwtGenerator.GenerateToken(user.Id, user.UserName);
                return IdentityResult.Success(new UserIdentity()
                {
                    JwtToken = token,
                    Info = new UserInfo()
                    {
                        Id = user.Id,
                    }
                });
            }
            string randomPassword = Guid.NewGuid().ToString().Substring(0, 11).Replace("-","_");
            var hashResult= _hasher.HashWithSHA256Algo(randomPassword);
            User newUser = User.CreateUser("user"+Guid.NewGuid().ToString().Substring(0,6).Replace("-","_"),
                userInfo.Name, hashResult.PasswordHash, hashResult.Salt, userInfo.Email);
            newUser.UploadAvatar(userInfo.UrlAvatar);
            await _db.Users.AddAsync(newUser);
            await _db.SaveChangesAsync();
            var tokenRegister =  _jwtGenerator.GenerateToken(newUser.Id, newUser.UserName);
            return IdentityResult.Success(new UserIdentity()
            {
                JwtToken = tokenRegister,
                Info = new UserInfo()
                {
                    Id = newUser.Id,
                }
            });
        }
        public async Task<Result<UserIdentity>> RegisterAsync(UserForRegisterCommand newUser)
        {
            if (await _db.Users.AnyAsync(p => p.UserName == newUser.UserName))
                return Result<UserIdentity>.Fail("UserName is existing in database");
            var hashPassWordResult = _hasher.HashWithSHA256Algo(newUser.Password);
            User newUserEntity = User.CreateUser(newUser.UserName, newUser.Name, hashPassWordResult.PasswordHash, hashPassWordResult.Salt);
            await _db.Users.AddAsync(newUserEntity);
            await _db.SaveChangesAsync();
            string tokenForRegister = _jwtGenerator.GenerateToken(newUserEntity.Id, newUserEntity.UserName);
            return IdentityResult.Success(new UserIdentity()
            {
                JwtToken = tokenForRegister,
                Info = new UserInfo()
                {
                    Id = newUserEntity.Id,
                }
            });
        }

    }
}
