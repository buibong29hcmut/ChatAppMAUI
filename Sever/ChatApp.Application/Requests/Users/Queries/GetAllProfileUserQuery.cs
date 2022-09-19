using ChatApp.Application.Cores.Queries;
using ChatApp.Application.Response.Users;
using ChatApp.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Requests.Users.Queries
{
    public class GetAllProfileUserQuery: IQuery<Result<PageList<ProfileUserResponseWithOperation>>>
    {
        public Guid UserId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
  
}
