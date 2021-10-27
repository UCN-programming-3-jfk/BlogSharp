﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using WebApiClient.DTOs;

namespace WebApiClient
{
public class BlogSharpApiClient
{
        private RestClient _restClient;
        public BlogSharpApiClient(string uri) => _restClient = new RestClient(new Uri(uri));

        public async Task<int> CreateAuthorAsync(Author entity, string password)
        {
            var response = await _restClient.RequestAsync<int>(Method.POST, "authors", new {entity, Password=password});

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error creating author with email={entity.Email}. Message was {response.StatusDescription}");
            }
            return response.Data;
        }
        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            var response = await _restClient.RequestAsync<IEnumerable<Author>>(Method.GET, $"authors");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all authors. Message was {response.StatusDescription}");
            }
            return response.Data;
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            var response = await _restClient.RequestAsync<Author>(Method.GET, $"authors/{id}");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all authors. Message was {response.StatusDescription}");
            }
            return response.Data;
        }

        public async Task<bool> UpdateAuthorAsync(Author entity)
        {
            var response = await _restClient.RequestAsync<bool>(Method.PUT, "authors", entity);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error creating author with email={entity.Email}. Message was {response.StatusDescription}");
            }
            return response.Data;
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAuthorPasswordAsync(string email, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<int> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateBlogPostAsync(BlogPost entity)
        {
            var response = await _restClient.ExecuteAsync<int>(new RestRequest());

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all authors. Message was {response.StatusDescription}");
            }
            return response.Data;
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BlogPost> GetBlogPostByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateBlogPostAsync(BlogPost entity)
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
             var request = new RestRequest(resource, method);
            if (body != null)
            {
                request.AddJsonBody(JsonSerializer.Serialize(body));
            }
            return await client.ExecuteAsync<T>(request, method);
        }

    }
}
