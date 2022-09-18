using ChatApp.Application.Cores.Queries;
using ChatApp.Application.Interfaces.DAL;
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
    public class GetProfileUserQueryHandler:IQueryHandler<GetProfileUserQuery,Result<ProfileUserResponse>
    {
        private readonly IDistributedCache _cache;
        private readonly IDbFactory _factory;
        public GetProfileUserQueryHandler(IDistributedCache cache, IDbFactory factory)
        {
            _cache = cache;
            _factory = factory;
        }

        public async Task<Result<ProfileUserResponse>> Handle(GetProfileUserQuery request, CancellationToken cancellationToken)
        {
            string query = "SELECT \"UserName\",  \"UrlAvatar\", \"Name\"\r\n\tFROM public.\"Users\"";
            using (var db = _factory.CreateConnection())
            {
                var profile =await db.QueryFirstAsync<ProfileUserResponse>(query);
                return Result<ProfileUserResponse>.Success(profile);
            }
        }
    }
}
