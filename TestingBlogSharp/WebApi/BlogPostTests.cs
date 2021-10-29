using DataAccess.Repositories;
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

        private int NewAuthorId { get; set; }
        private List<int> _createdBlogPostIds = new();

        [SetUp]
        public void Setup()
        {
            //SetUp methods cannot run async :(
            NewAuthorId = Task.Run(() => new  BlogSharpApiClient(Configuration.WEB_API_URL).CreateAuthorAsync(new AuthorDto() { Email = "New author for post tests", BlogTitle = "Title of my blog"}, "password")).Result;
        }

        [TearDown]
        public void CleanUp()
        {
            //TearDown methods cannot run async :(
            Task.Run(() => new AuthorRepository(Configuration.WEB_API_URL).DeleteAsync(NewAuthorId)).Wait();
        }

        [Test]
        public async Task GetBlogPostsAsync()
        {
            //ARRANGE
            var blogpostRep = new BlogPostRepository(Configuration.WEB_API_URL);
            var newBlogPost = new BlogPost() { PostTitle = "My post title", AuthorId = NewAuthorId, PostContent = "Content", PostCreationDate = DateTime.Now };
            var newBlogPostId = await blogpostRep.CreateAsync(newBlogPost);
            _createdBlogPostIds.Add(newBlogPostId);
            //ACT
            var blogposts = await blogpostRep.GetAllAsync();
            //ASSERT
            Assert.IsTrue(blogposts.Count() > 0, "No blog posts returned");
        }

        [Test]
        public async Task CreateBlogPostAsync()
        {
            //ARRANGE
            var blogpostRep = new BlogPostRepository(Configuration.WEB_API_URL);
            var newBlogPost = new BlogPost() { PostTitle = "My post title", AuthorId = NewAuthorId, PostContent = "Content", PostCreationDate = DateTime.Now };
            //ACT
            var newBlogPostId = await blogpostRep.CreateAsync(newBlogPost);
            _createdBlogPostIds.Add(newBlogPostId);
            //ASSERT
            Assert.IsTrue(newBlogPostId > 0, "Created blogpost ID not returned");
        }

        [Test]
        public async Task DeleteBlogPostAsync()
        {
            //ARRANGE
            var blogpostRep = new BlogPostRepository(Configuration.WEB_API_URL);
            var newBlogPost = new BlogPost() { PostTitle = "My post title", AuthorId = NewAuthorId, PostContent = "Content", PostCreationDate = DateTime.Now };
            var newId = await blogpostRep.CreateAsync(newBlogPost);
            _createdBlogPostIds.Add(newId);
            //ACT
            bool deleted = await blogpostRep.DeleteAsync(newId);
            //ASSERT
            Assert.IsTrue(deleted, "BlogPost not deleted");
        }

        [Test]
        public async Task FindBlogPostAsync()
        {
            //ARRANGE
            var blogpostRep = new BlogPostRepository(Configuration.WEB_API_URL);
            var newBlogPost = new BlogPost() { PostTitle = "My post title", AuthorId = NewAuthorId, PostContent = "Content", PostCreationDate = DateTime.Now };
            var newId = await blogpostRep.CreateAsync(newBlogPost);
            _createdBlogPostIds.Add(newId);
            //ACT
            var refoundBlogPost = await blogpostRep.GetByIdAsync(newId);
            //ASSERT
            Assert.IsTrue(refoundBlogPost != null, "BlogPost not found again");
        }

        [Test]
        public async Task UpdateBlogPostAsync()
        {
            //ARRANGE
            string updatedPostTitle = "A great new post";
            string updatedPostContent = "Even greater content";
            DateTime updatedPostDate = DateTime.Now.AddDays(-3);
            var blogpostRep = new BlogPostRepository(Configuration.WEB_API_URL);
            var newBlogPost = new BlogPost() { PostTitle = "My post title", AuthorId = NewAuthorId, PostContent = "Content", PostCreationDate = DateTime.Now };
            newBlogPost.Id = await blogpostRep.CreateAsync(newBlogPost);
            newBlogPost.PostTitle = updatedPostTitle;
            newBlogPost.PostContent = updatedPostContent;
            newBlogPost.PostCreationDate = updatedPostDate;
            _createdBlogPostIds.Add(newBlogPost.Id);
            //ACT
            await blogpostRep.UpdateAsync(newBlogPost);
            //ASSERT
            var refoundBlogPost = await blogpostRep.GetByIdAsync(newBlogPost.Id);
            Assert.IsTrue(refoundBlogPost.PostTitle == updatedPostTitle && refoundBlogPost.PostContent == updatedPostContent
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