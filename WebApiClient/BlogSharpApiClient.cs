using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApiClient.DTOs;

namespace WebApiClient
{
 
    /// <summary>
    /// This class acts as a proxy for the Web API, 
    /// encapsulating all the HTTP communication, 
    /// so it can be used from any GUI 
    /// to access BlogPost and Author CRUD methods
    /// without knowledge of how to interact with a RESTful service.
    /// </summary>
    public class BlogSharpApiClient : IBlogSharpApiClient
    {
        private RestClient _restClient;
        public BlogSharpApiClient(string uri) => _restClient = new RestClient(new Uri(uri));

        public async Task<int> CreateAuthorAsync(AuthorDto entity)
        {
            var response = await _restClient.RequestAsync<int>(Method.POST, "authors", entity);
            if (!response.IsSuccessful)
            {
                throw new Exception($"Error creating author with email={entity.Email}. Message was {response.Content}");
            }
            return response.Data;
        }
        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var response = await _restClient.RequestAsync<IEnumerable<AuthorDto>>(Method.GET, $"authors");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all authors. Message was {response.Content}");
            }
            return response.Data;
        }

        public async Task<AuthorDto> GetAuthorByIdAsync(int id)
        {
            var response = await _restClient.RequestAsync<AuthorDto>(Method.GET, $"authors/{id}");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all authors. Message was {response.Content}");
            }
            return response.Data;
        }

        public async Task<bool> UpdateAuthorAsync(AuthorDto entity)
        {
            var response = await _restClient.RequestAsync(Method.PUT, $"authors/{entity.Id}", entity);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                throw new Exception($"Error updating author with email={entity.Email}. Message was {response.Content}");
            }

        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var response = await _restClient.RequestAsync(Method.DELETE, $"authors/{id}", null);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                throw new Exception($"Error deleting author with id={id}. Message was {response.Content}");
            }
        }

        public async Task<AuthorDto> GetAuthorByEmailAsync(string email)
        {
            var response = await _restClient.RequestAsync<IEnumerable<AuthorDto>>(Method.GET, $"authors?email={email}");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all authors. Message was {response.Content}");
            }
            return response.Data.FirstOrDefault();
        }

        public async Task<bool> UpdateAuthorPasswordAsync(AuthorDto author)
        {
            var response = await _restClient.RequestAsync(Method.PUT, $"authors/{author.Id}/Password", author);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                throw new Exception($"Error updating password for author with email={author.Email}. Message was {response.Content}");
            }
        }

        public async Task<int> LoginAsync(AuthorDto author)
        {
            var response = await _restClient.RequestAsync<int>(Method.POST, $"logins", author);
            if (!response.IsSuccessful)
            {
                throw new Exception($"Error logging in for author with email={author.Email}. Message was {response.Content}");
            }
            return response.Data;
        }

        public async Task<int> CreateBlogPostAsync(BlogPostDto entity)
        {
            var response = await _restClient.RequestAsync<int>(Method.POST, $"blogposts", entity);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error creating blogpost. Message was {response.Content}");
            }
            return response.Data;
        }

        public async Task<IEnumerable<BlogPostDto>> GetAllBlogPostsAsync()
        {
            var response = await _restClient.RequestAsync<IEnumerable<BlogPostDto>>(Method.GET, $"blogposts");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all blogposts. Message was {response.Content}");
            }
            return response.Data;
        }

        public async Task<IEnumerable<BlogPostDto>> Get10LatestBlogPostsAsync()
        {
            var request = new RestRequest($"blogposts", Method.GET, DataFormat.Json);
            request.AddParameter("filter", "getlatest10");
            var response = await _restClient.ExecuteAsync<IEnumerable<BlogPostDto>>(request);
            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving latest blogposts. Message was {response.Content}");
            }
            return response.Data;
        }


        public async Task<BlogPostDto> GetBlogPostByIdAsync(int id)
        {
            var response = await _restClient.RequestAsync<BlogPostDto>(Method.GET, $"blogposts/{id}");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving blogpost with id {id} . Message was {response.Content}");
            }
            return response.Data;
        }

        public async Task<bool> UpdateBlogPostAsync(BlogPostDto entity)
        {
            var response = await _restClient.RequestAsync(Method.PUT, $"blogposts/{entity.Id}", entity);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                throw new Exception($"Error updating blogpost with id={entity.Id}. Message was {response.Content}");
            }

        }

        public async Task<bool> DeleteBlogPostAsync(int id)
        {
            var response = await _restClient.RequestAsync(Method.DELETE, $"blogposts/{id}", null);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                throw new Exception($"Error deleting blogpost with id={id}. Message was {response.Content}");
            }
        }
    }
}