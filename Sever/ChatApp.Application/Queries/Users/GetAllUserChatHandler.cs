﻿using ChatApp.Application.Cores.Queries;
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
            string query = "SELECT  \"UserName\",  \"UrlAvatar\",  \"Name\"\r\n\tFROM public.\"Users\"\r\n\tWHERE \"Id\"!=@userId\r\n\tORDER BY \"UserName\"\r\n\tLIMIT @pageSize\r\n\tOFFSET (@pageNumber-1)*@pageSize";
            using (var connection = _factory.CreateConnection())
            {
                var allProfile = (await connection.QueryAsync<ProfileUserResponse>(query, 
                    new 
                    {
                        userId = para.UserId,
                        pageSize=para.PageSize,
                        pageNumber=para.PageNumber
                    })).ToList();
                PageList<ProfileUserResponseWithOperation> result = new PageList<ProfileUserResponseWithOperation>(allProfile.Count,para.PageNumber,para.PageSize);
                foreach(var profile in allProfile)
                {
                    ProfileUserResponseWithOperation profileUserResponseWithOperation =
                        new ProfileUserResponseWithOperation()
                        {
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
