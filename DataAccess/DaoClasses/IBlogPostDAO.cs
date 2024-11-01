using DataAccess.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DataAccess.DaoClasses
{

    /// <summary>
    /// An interface which defines which functionality 
    /// a BlogPostRepository should provide
    /// </summary>
    public interface IBlogPostDAO {
        
        Task<int> CreateAsync(BlogPost entity);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost> GetByIdAsync(int id);
        Task<bool> UpdateAsync(BlogPost entity);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<BlogPost>> GetByAuthorIdAsync(int authorId);
        Task<IEnumerable<BlogPost>> Get10LatestAsync();
        Task<IEnumerable<BlogPost>> GetByPartOfTitleOrContentAsync(string partOfTitleOrContent);
    }
}