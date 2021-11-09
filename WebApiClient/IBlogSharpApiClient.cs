using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiClient.DTOs;

namespace WebApiClient
{
    public interface IBlogSharpApiClient
    {
        Task<int> CreateAuthorAsync(AuthorDto entity);
        Task<int> CreateBlogPostAsync(BlogPostDto entity);
        Task<bool> DeleteAuthorAsync(int id);
        Task<bool> DeleteBlogPostAsync(int id);
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
        Task<IEnumerable<BlogPostDto>> GetAllBlogPostsAsync();
        Task<IEnumerable<BlogPostDto>> Get10LatestBlogPostsAsync();
        Task<AuthorDto> GetAuthorByIdAsync(int id);
        Task<BlogPostDto> GetBlogPostByIdAsync(int id);
        Task<int> LoginAsync(AuthorDto author);
        Task<bool> UpdateAuthorAsync(AuthorDto entity);
        Task<bool> UpdateAuthorPasswordAsync(AuthorDto author);
        Task<bool> UpdateBlogPostAsync(BlogPostDto entity);
    }
}