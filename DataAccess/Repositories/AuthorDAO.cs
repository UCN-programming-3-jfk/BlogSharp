using Dapper;
using DataAccess.Authentication;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Implementation of the IAuthorRepository
    /// </summary>
    public class AuthorDAO : BaseDAO, IAuthorDAO
    {
        public AuthorDAO(string connectionstring) : base(connectionstring) { }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var query = "DELETE FROM Author WHERE Id=@Id";
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting author with id {id}: '{ex.Message}'.", ex);
            }
        }

public async Task<IEnumerable<Author>> GetAllAsync()
{
    try
    {
        var query = "SELECT * FROM Author";
        using var connection = CreateConnection();
        return (await connection.QueryAsync<Author>(query)).ToList();
    }
    catch (Exception ex)
    {
        throw new Exception($"Error getting all authors: '{ex.Message}'.", ex);
    }
}

        public async Task<Author> GetByIdAsync(int id)
        {
            try
            {
                var query = "SELECT * FROM Author WHERE Id=@Id";
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<Author>(query, new { id });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting author with id {id}: '{ex.Message}'.", ex);
            }
        }


        public async Task<bool> UpdateAsync(Author entity)
        {
            try
            {
                var query = "UPDATE Author SET Email=@Email, BlogTitle=@BlogTitle WHERE Id=@Id;";
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, entity) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating author: '{ex.Message}'.", ex);
            }
        }

        public async Task<int> CreateAsync(Author entity, string password)
        {
            try
            {
                var query = "INSERT INTO Author (Email, BlogTitle, PasswordHash) OUTPUT INSERTED.Id VALUES (@Email, @BlogTitle, @PasswordHash);";
                var passwordHash = BCryptTool.HashPassword(password);
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<int>(query, new { Email = entity.Email, BlogTitle = entity.BlogTitle, PasswordHash = passwordHash });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating new author: '{ex.Message}'.", ex);
            }

        }

        public async Task<int> LoginAsync(string email, string password)
        {
            try
            {
                var query = "SELECT Id, PasswordHash FROM Author WHERE Email=@Email";
                using var connection = CreateConnection();

                var authorTuple = await connection.QueryFirstOrDefaultAsync<AuthorTuple>(query, new { Email = email});
                if (authorTuple != null && BCryptTool.ValidatePassword(password, authorTuple.PasswordHash))
                {
                    return authorTuple.Id;
                }
                return -1;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error logging in for author with email {email}: '{ex.Message}'.", ex);
            }
        }

        public async Task<bool> UpdatePasswordAsync(string email, string oldPassword, string newPassword)
        {
            try
            {
                var query = "UPDATE Author SET PasswordHash=@NewPasswordHash WHERE Id=@Id;";
                var id = await LoginAsync(email, oldPassword);
                if (id > 0)
                {
                    var newPasswordHash = BCryptTool.HashPassword(newPassword);
                    using var connection = CreateConnection();
                    return await connection.ExecuteAsync(query, new { Id = id, NewPasswordHash = newPasswordHash }) > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating author: '{ex.Message}'.", ex);
            }
        }

        public async Task<Author> GetByEmailAsync(string email)
        {
            try
            {
                var query = "SELECT * FROM Author WHERE Email=@Email";
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<Author>(query, new { email });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting author with email {email}: '{ex.Message}'.", ex);
            }
        }

        internal class AuthorTuple 
        {
            public int Id;
            public string PasswordHash;
        }
    }
}