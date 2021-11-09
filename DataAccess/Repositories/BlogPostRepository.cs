using Dapper;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class BlogPostRepository : BaseRepository, IBlogPostRepository
    {
        public BlogPostRepository(string connectionstring) : base(connectionstring) { }

        public async Task<int> CreateAsync(BlogPost entity)
        {
            try
            {
                var query = "INSERT INTO BlogPost (AuthorId, PostTitle, PostContent, PostCreationDate) OUTPUT INSERTED.Id VALUES (@AuthorId, @PostTitle, @PostContent, @PostCreationDate);";
                using var connection = CreateConnection();
                return (await connection.QuerySingleAsync<int>(query, entity));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting new blog post: '{ex.Message}'.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var query = "DELETE FROM BlogPost WHERE Id=@Id";
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting blog post with id {id}: '{ex.Message}'.", ex);
            }
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            try
            {
                var query = "SELECT * FROM BlogPost";
                using var connection = CreateConnection();
                return (await connection.QueryAsync<BlogPost>(query)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting all blog posts: '{ex.Message}'.", ex);
            }
        }

        public async Task<BlogPost> GetByIdAsync(int id)
        {
            try
            {
                var query = "SELECT * FROM BlogPost WHERE Id=@Id";
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<BlogPost>(query, new { id });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting blog post with id {id}: '{ex.Message}'.", ex);
            }
        }

        public async Task<bool> UpdateAsync(BlogPost entity)
        {
            try
            {
                var query = "UPDATE BlogPost SET AuthorId=@AuthorId, PostTitle=@PostTitle, PostContent=@PostContent , PostCreationDate=@PostCreationDate WHERE Id=@Id;";
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, entity) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating blog post: '{ex.Message}'.", ex);
            }
        }

        public async Task<IEnumerable<BlogPost>> GetByAuthorIdAsync(int authorId)
        {
            try
            {
                var query = "SELECT * FROM BlogPost WHERE AuthorId=@AuthorId";
                using var connection = CreateConnection();
                return await connection.QueryAsync<BlogPost>(query, new { authorId });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting blog posts with authorid {authorId}: '{ex.Message}'.", ex);
            }
        }

        public async Task<IEnumerable<BlogPost>> Get10LatestAsync()
        {
            try
            {
                var query = "SELECT top 10 * FROM BlogPost ORDER BY PostCreationDate DESC";
                using var connection = CreateConnection();
                return await connection.QueryAsync<BlogPost>(query, new { amount=10 });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting latest blog posts: '{ex.Message}'.", ex);
            }
        }
    }
}