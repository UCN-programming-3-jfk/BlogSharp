using System.Data;
using System.Data.SqlClient;

namespace DataAccess.DaoClasses
{
    /// <summary>
    /// This abstract class defines the core of all repositories
    /// - a connectionstring variable which is set using the constructor
    /// - a method which can create a needed connection for subclasses of the BaseRepository
    /// </summary>
    public abstract class BaseDAO
    {
        private string _connectionstring;

        protected BaseDAO(string connectionstring) => _connectionstring = connectionstring;

        protected IDbConnection CreateConnection() => new SqlConnection(_connectionstring);
    }
}