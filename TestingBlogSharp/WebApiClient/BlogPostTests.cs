using DataAccess.DaoClasses;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Model;
using System;
using WebApiClient;
using WebApiClient.DTOs;

namespace TestingBlogSharp.WebApi
{
    public class BlogPostTests
    {
        private AuthorDto _newAuthor;
        private BlogPostDto _newBlogPost;
        private List<int> _createdBlogPostIds = new();
        private BlogSharpApiClient _client = new BlogSharpApiClient(Configuration.WEB_API_URL);

        private async Task<AuthorDto> CreateNewAuthorAsync()
        {
            _newAuthor = new AuthorDto() { Email = "New author for post tests", BlogTitle = "Title of my blog", Password = "password" };
            _newAuthor.Id = await _client.CreateAuthorAsync(_newAuthor);
            return _newAuthor;
        }

        private async Task<BlogPostDto> CreateNewBlogPostAsync(int authorId)
        {
            _newBlogPost = new BlogPostDto() { AuthorId=authorId, PostContent="Post content", PostCreationDate=DateTime.Now, PostTitle="Post title"};
            _newBlogPost.Id = await _client.CreateBlogPostAsync(_newBlogPost);
            return _newBlogPost;
        }


        [OneTimeSetUp]
        public async Task OneTimeSetup() => await CreateNewAuthorAsync();

        [SetUp]
        public async Task Setup() => await CreateNewBlogPostAsync(_newAuthor.Id);

        [TearDown]
        public void CleanUp()
        {
            Parallel.ForEach(_createdBlogPostIds, async (id) => await _client.DeleteBlogPostAsync(id));
        }

        [OneTimeTearDown]
        public async Task OneTimeCleanup() => await _client.DeleteAuthorAsync(_newAuthor.Id);

        [Test]
        public async Task GetBlogPostsAsync()
        {
            //ARRANGE 
            //ACT
            var blogposts = await _client.GetAllBlogPostsAsync();
            //ASSERT
            Assert.That(blogposts.Count() > 0, "No blog posts returned");
        }

        [Test]
        public async Task CreateBlogPostAsync()
        {
            //ARRANGE & ACT done in setup
            //ASSERT
            Assert.That(_newBlogPost.Id > 0, "Created blogpost ID not returned");
        }

        [Test]
        public async Task DeleteBlogPostAsync()
        {
            //ARRANGE done in setup

            //ACT
            bool deleted = await _client.DeleteBlogPostAsync(_newBlogPost.Id);
            //ASSERT
            Assert.That(deleted, "BlogPost not deleted");
        }

        [Test]
        public async Task FindBlogPostAsync()
        {
            //ARRANGE done in setup
           
            //ACT
            var refoundBlogPost = await _client.GetBlogPostByIdAsync(_newBlogPost.Id);
            //ASSERT
            Assert.That(refoundBlogPost != null, "BlogPost not found again");
        }

        [Test]
        public async Task UpdateBlogPostAsync()
        {
            //ARRANGE
            string updatedPostTitle = "A great new post";
            string updatedPostContent = "Even greater content";
            DateTime updatedPostDate = DateTime.Now.AddDays(-3);
            
            _newBlogPost.PostTitle = updatedPostTitle;
            _newBlogPost.PostContent = updatedPostContent;
            _newBlogPost.PostCreationDate = updatedPostDate;
            _createdBlogPostIds.Add(_newBlogPost.Id);
            //ACT
            await _client.UpdateBlogPostAsync(_newBlogPost);
            //ASSERT
            var refoundBlogPost = await _client.GetBlogPostByIdAsync(_newBlogPost.Id);
            Assert.That(refoundBlogPost.PostTitle == updatedPostTitle && refoundBlogPost.PostContent == updatedPostContent
                && refoundBlogPost.PostCreationDate.Year == updatedPostDate.Year
                && refoundBlogPost.PostCreationDate.Month == updatedPostDate.Month
                && refoundBlogPost.PostCreationDate.Day == updatedPostDate.Day
                && refoundBlogPost.PostCreationDate.Hour == updatedPostDate.Hour
                && refoundBlogPost.PostCreationDate.Minute == updatedPostDate.Minute
                && refoundBlogPost.PostCreationDate.Second == updatedPostDate.Second
                , "BlogPost not updated");
        }
    }
}