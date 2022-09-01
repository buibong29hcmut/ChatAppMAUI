using ChatApp.Application.Interfaces.DAL;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Contexts
{
    public class DbFactory:IDbFactory,IDisposable
    {
        private readonly IConfiguration _configuration;
        public DbFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString=_configuration.GetConnectionString("ChatDb");
        }
        private IDbConnection _connection { get;set; }
        private string ConnectionString { get;  set; }
        public IDbConnection CreateConnection() => CreateConnection(ConnectionString);
        public IDbConnection CreateConnection(string connectionString)
        {
            if (this._connection == null)
            {
                this._connection = new NpgsqlConnection(connectionString);
                this._connection.Open();
            }
            return this._connection;
        }
        public void Dispose()
        {
            if (this._connection != null && this._connection.State == ConnectionState.Open)
            {
                this._connection.Dispose();
            }
        }


    }
}
