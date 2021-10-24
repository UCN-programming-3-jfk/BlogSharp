using Dapper;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class AuthorRepository : BaseRepository, IAuthorRepository
    {
        public AuthorRepository(string connectionstring) : base(connectionstring) { }

        public async Task<int> CreateAsync(Author entity)
        {
            try
            {
                var query = "INSERT INTO Author (Email, BlogTitle, PasswordHash) OUTPUT INSERTED.Id VALUES (@Email, @BlogTitle, @PasswordHash);";
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<int>(query, entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting new author: '{ex.Message}'.", ex);
            }
        }

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
                var query = "UPDATE Author SET Email=@Email, PasswordHash=@PasswordHash, BlogTitle=@BlogTitle WHERE Id=@Id;";
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, entity) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating author: '{ex.Message}'.", ex);
            }
        }
    }
}