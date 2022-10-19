using ChatApp.Application.Cores.Queries;
using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Interfaces.Services;
using ChatApp.Application.Requests.Users.Queries;
using ChatApp.Application.Response.Users;
using ChatApp.Share.Wrappers;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Queries.Users
{
    public class GetAllUserChatHandler:IQueryHandler<GetAllProfileUserQuery, Result<PageList<ProfileUserResponseWithOperation>>>
    {
        private readonly IUserOperation _operation;
        private readonly IDbFactory _factory;
        public GetAllUserChatHandler(IUserOperation operation, IDbFactory factory)
        {
            _operation = operation;
            _factory = factory;
        }
        public async Task<Result<PageList<ProfileUserResponseWithOperation>>> Handle(GetAllProfileUserQuery para, CancellationToken cancellationToken)
        {
            string query = "SELECT \"Id\", \"UserName\",  \"UrlAvatar\",  \"Name\"" +
                           "FROM public.\"Users\"" +
                           "WHERE \"Id\"!=@userId\"" +
                           "ORDER BY \"UserName\"" +
                           "LIMIT @pageSize" +
                           "OFFSET (@pageNumber-1)*@pageSize";
            string countUserQuery="SELECT COUNT( \"UserName\") FROM public.\"Users\"";
            using (var connection = _factory.CreateConnection())
            {
                var allProfile = await connection.QueryAsync<ProfileUserResponse>(query, 
                    new 
                    {
                        userId = para.UserId,
                        pageSize=para.PageSize,
                        pageNumber=para.PageNumber
                    });
                var countUser = await connection.QueryFirstAsync<int>(countUserQuery);
                PageList<ProfileUserResponseWithOperation> result = new PageList<ProfileUserResponseWithOperation>(countUser, para.PageNumber,para.PageSize);
                foreach(var profile in allProfile)
                {
                    ProfileUserResponseWithOperation profileUserResponseWithOperation =
                        new ProfileUserResponseWithOperation()
                        {   Id=profile.Id,
                            UserName = profile.UserName,
                            Name = profile.Name,
                            IsOnline = await _operation.IsUserOnline(profile.UserName),
                            UrlAvatar = profile.UrlAvatar
                        };
                    result.Add(profileUserResponseWithOperation);
                }
                return Result<PageList<ProfileUserResponseWithOperation>>.Success(result);
               

            }
        }
    }
}
