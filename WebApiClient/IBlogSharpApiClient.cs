using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiClient.DTOs;

namespace WebApiClient
{
    /// <summary>
    /// This interface defines what functionality is necessary
    /// for an object to provide access to the BlogSharp business layer (controller)
    /// </summary>
    public interface IBlogSharpApiClient
    {
        Task<AuthorDto> GetAuthorByIdAsync(int id);
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
        Task<int> CreateAuthorAsync(AuthorDto entity);
        Task<bool> UpdateAuthorAsync(AuthorDto entity);
        Task<int> LoginAsync(AuthorDto author);
        Task<bool> DeleteAuthorAsync(int id);
        Task<bool> UpdateAuthorPasswordAsync(AuthorDto author);

        Task<int> CreateBlogPostAsync(BlogPostDto entity);
        Task<BlogPostDto> GetBlogPostByIdAsync(int id);
        Task<IEnumerable<BlogPostDto>> GetAllBlogPostsAsync();
        Task<IEnumerable<BlogPostDto>> Get10LatestBlogPostsAsync();
        Task<bool> DeleteBlogPostAsync(int id);
        Task<IEnumerable<BlogPostDto>> GetBlogPostsFromAuthorIdAsync(int authorId);
        Task<bool> UpdateBlogPostAsync(BlogPostDto entity);
        Task<IEnumerable<BlogPostDto>> GetBlogPostsFromPartOfTitleOrContentAsync(string partOfTitleOrContent);
    }
}