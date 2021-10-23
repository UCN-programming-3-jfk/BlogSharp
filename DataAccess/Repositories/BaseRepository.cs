using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Repositories
{
    public abstract class BaseRepository
    {
        private string _connectionstring;

        protected BaseRepository(string connectionstring) => _connectionstring = connectionstring;

        protected IDbConnection CreateConnection() => new SqlConnection(_connectionstring);
    }
}