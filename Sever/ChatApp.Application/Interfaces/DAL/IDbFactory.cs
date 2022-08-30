using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Interfaces.DAL
{
    public interface IDbFactory
    {
        IDbConnection CreateConnection();
        IDbConnection CreateConnection(string connectionString);
    }
}
