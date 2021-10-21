using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public abstract class BaseRepository
    {
        private string _connectionstring;

        protected BaseRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        protected IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionstring);
        }
    }
}
