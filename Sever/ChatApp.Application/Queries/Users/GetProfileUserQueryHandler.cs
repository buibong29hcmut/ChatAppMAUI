using ChatApp.Application.Cores.Queries;
using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Interfaces.Services;
using ChatApp.Application.Requests.Users.Queries;
using ChatApp.Application.Response.Users;
using ChatApp.Share.Wrappers;
using Dapper;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Queries.Users
{
    public class GetProfileUserQueryHandler:IQueryHandler<GetProfileUserQuery,Result<ProfileUserResponseWithOperation>>
    {
        private readonly IDistributedCache _cache;
        private readonly IDbFactory _factory;
        private readonly IUserOperation _operation;
        public GetProfileUserQueryHandler(IDistributedCache cache,
            IDbFactory factory,
            IUserOperation operation)
        {
            _cache = cache;
            _factory = factory;
            _operation = operation;
        }

        public async Task<Result<ProfileUserResponseWithOperation>> Handle(GetProfileUserQuery request, CancellationToken cancellationToken)
        {
            string query = "SELECT \"Id\", \"UserName\",  \"UrlAvatar\",  \"Name\"\r\nFROM public.\"Users\"\r\nWHERE \"Id\"=@UserId\r\nLIMIT 1";
            using (var db = _factory.CreateConnection())
            {
                var profile =await db.QueryFirstAsync<ProfileUserResponse>(query, new
                {
                    UserId= request.UserId
                });
                var profileWithOperation = new ProfileUserResponseWithOperation()
                {   Id=profile.Id,
                    UrlAvatar = profile.UrlAvatar,
                    IsOnline = await _operation.IsUserOnline(profile.UserName),
                    Name = profile.Name,
                    UserName = profile.UserName
                };
                return Result<ProfileUserResponseWithOperation>.Success(profileWithOperation);
            }
        }
    }
}
