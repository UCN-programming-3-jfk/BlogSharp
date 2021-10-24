using DataAccess.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IAuthorRepository
    {
        Task<int> CreateAsync(Author entity, string password);
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Author entity);
        Task<bool> DeleteAsync(int id);
        
        Task<bool> UpdatePasswordAsync(string email, string oldPassword, string newPassword);
        Task<int> LoginAsync(string email, string password);
    }
}