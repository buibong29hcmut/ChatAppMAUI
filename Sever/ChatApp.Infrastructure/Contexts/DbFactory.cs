using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Contexts
{
    public class DbFactory:IDbFactory
    {
        private readonly IConfiguration _configuration;
        public DbFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString=_configuration.GetConnectionString("ChatDb");
        }
        private string ConnectionString { get;  set; }
        public IDbConnection CreateConnection() => CreateConnection(ConnectionString);
        public IDbConnection CreateConnection(string connectionString) => new SqlConnection(connectionString);

    }
}
