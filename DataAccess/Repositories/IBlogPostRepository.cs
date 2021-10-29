using DataAccess.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public interface IBlogPostRepository {
        //Default CRUD methods for Repository (DAO pattern)
        Task<int> CreateAsync(BlogPost entity);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost> GetByIdAsync(int id);
        Task<bool> UpdateAsync(BlogPost entity);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<BlogPost>> GetByAuthorIdAsync(int authorId);
    }
}