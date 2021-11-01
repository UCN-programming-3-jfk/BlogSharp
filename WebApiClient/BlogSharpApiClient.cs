﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using WebApiClient.DTOs;

namespace WebApiClient
{
public class BlogSharpApiClient
{
        private RestClient _restClient;
        public BlogSharpApiClient(string uri) => _restClient = new RestClient(new Uri(uri));

        public async Task<int> CreateAuthorAsync(AuthorDto entity)
        {
            //var request = new RestRequest("authors", DataFormat.Json);
            //request.AddJsonBody(entity);
            //return await _restClient.PostAsync<int>(request);
            var response = await _restClient.RequestAsync<int>(Method.POST, "authors", entity);
            if (!response.IsSuccessful)
            {
                throw new Exception($"Error creating author with email={entity.Email}. Message was {response.ErrorException?.Message}");
            }
            return response.Data;
        }
        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var response = await _restClient.RequestAsync<IEnumerable<AuthorDto>>(Method.GET, $"authors");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all authors. Message was {response.ErrorException?.Message}");
            }
            return response.Data;
        }

        public async Task<AuthorDto> GetAuthorByIdAsync(int id)
        {
            var response = await _restClient.RequestAsync<AuthorDto>(Method.GET, $"authors/{id}");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all authors. Message was {response.ErrorException?.Message}");
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
                throw new Exception($"Error updating author with email={entity.Email}. Message was {response.ErrorException?.Message}");
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
                throw new Exception($"Error delting author with id={id}. Message was {response.ErrorException?.Message}");
            }
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
                throw new Exception($"Error updating password for author with email={author.Email}. Message was {response.ErrorException?.Message}");
            }
        }

        public async Task<int> LoginAsync(AuthorDto author)
        {
            var response = await _restClient.RequestAsync<int>(Method.POST, $"logins", author);
            if (!response.IsSuccessful)
            {
                throw new Exception($"Error deleting author with id={author.Id}. Message was {response.ErrorException?.Message}");
            }
            return response.Data;
        }

        public async Task<int> CreateBlogPostAsync(BlogPostDto entity)
        {
            var response = await _restClient.ExecuteAsync<int>(new RestRequest());

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all authors. Message was {response.ErrorException?.Message}");
            }
            return response.Data;
        }

        public async Task<IEnumerable<BlogPostDto>> GetAllBlogPostsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BlogPostDto> GetBlogPostByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateBlogPostAsync(BlogPostDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteBlogPostAsync(int id)
        {
            throw new NotImplementedException();
        }
    }

    public static class RestClientExtensions
    {
        public static async Task<IRestResponse<T>> RequestAsync<T>(this RestClient client, Method method, string  resource = null, object body = null)
        {
             var request = new RestRequest(resource, method, DataFormat.Json);
            if (body != null)
            {
                request.AddJsonBody(JsonSerializer.Serialize(body));
            }
            return await client.ExecuteAsync<T>(request, method);
        }

        public static async Task<IRestResponse> RequestAsync(this RestClient client, Method method, string resource = null, object body = null)
        {
            var request = new RestRequest(resource, method, DataFormat.Json);
            if (body != null)
            {
                request.AddJsonBody(JsonSerializer.Serialize(body));
            }
            return await client.ExecuteAsync(request, method);
        }

    }
}
