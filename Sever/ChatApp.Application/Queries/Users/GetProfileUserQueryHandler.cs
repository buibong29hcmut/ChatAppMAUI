using ChatApp.Application.Interfaces.DAL;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Queries.Users
{
    public class GetProfileUserQueryHandler
    {
        private readonly IDistributedCache _cache;
        private readonly IDbFactory _factory;
        public GetProfileUserQueryHandler(IDistributedCache cache, IDbFactory factory)
        {
            _cache = cache;
            _factory = factory;
        }
    }
}
