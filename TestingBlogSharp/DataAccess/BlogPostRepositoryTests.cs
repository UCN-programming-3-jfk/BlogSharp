using DataAccess.Repositories;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Model;
using System;

namespace TestingBlogSharp.DataAccess
{
    public class _blogPostRepositoryTests
    {
        //TODO: add test to test cascading delete of BlogPosts with their Author
        
        private AuthorRepository _authorRepository;
        private BlogPostRepository _blogPostRepository;
        private Author _newAuthor;
        private BlogPost _newBlogPost;

        [SetUp]
        public async Task SetupAsync()
        {
            _authorRepository = new AuthorRepository(Configuration.CONNECTION_STRING);
            _blogPostRepository= new BlogPostRepository(Configuration.CONNECTION_STRING);
            _newAuthor = new Author() { Email = "mail@myblog.com", BlogTitle = "Title of my blog" };
            _newAuthor.Id = await _authorRepository.CreateAsync(_newAuthor, "test1234");
            _newBlogPost = new BlogPost() { PostTitle = "My post title", AuthorId = _newAuthor.Id, PostContent = "Content", PostCreationDate = System.DateTime.Now };
            _newBlogPost.Id = await _blogPostRepository.CreateAsync(_newBlogPost);
        }

        [TearDown]
        public async Task CleanUpAsync()
        {
            await new AuthorRepository(Configuration.CONNECTION_STRING).DeleteAsync(_newAuthor.Id);
        }

        [Test]
        public async Task GetBlogPostsAsync()
        {
            //ARRANGE
            //ACT
            var blogposts = await _blogPostRepository.GetAllAsync();
            //ASSERT
            Assert.IsTrue(blogposts.Count() > 0, "No blog posts returned");
        }

        [Test]
        public void CreateBlogPost()
        {
            //ARRANGE
            //ACT
            //ASSERT
            Assert.IsTrue(_newBlogPost.Id > 0, "Created blogpost ID not returned");
        }

        [Test]
        public async Task DeleteBlogPostAsync()
        {
            //ARRANGE
            //ACT
            bool deleted = await _blogPostRepository.DeleteAsync(_newBlogPost.Id);
            //ASSERT
            Assert.IsTrue(deleted, "BlogPost not deleted");
        }

        [Test]
        public async Task FindBlogPostAsync()
        {
            //ARRANGE
            //ACT
            var refoundBlogPost = await _blogPostRepository.GetByIdAsync(_newBlogPost.Id);
            //ASSERT
            Assert.IsTrue(refoundBlogPost != null, "BlogPost not found again");
        }


        [Test]
        public async Task GetLatestBlogPostsAsync()
        {
            //ARRANGE
            for (int blogPostCounter = 0; blogPostCounter < 15; blogPostCounter++)
            {
                var blogPost = new BlogPost() {AuthorId = _newAuthor.Id, PostCreationDate = DateTime.Now.AddDays(-blogPostCounter), PostContent="Great content", PostTitle=$"Great title {blogPostCounter}" };
                await _blogPostRepository.CreateAsync(blogPost);
            }
            
            //ACT
            var refoundBlogPosts = await _blogPostRepository.Get10LatestAsync();
            //ASSERT
            Assert.IsTrue(refoundBlogPosts.Count() == 10, "BlogPost not found again");
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
            //ACT
            await _blogPostRepository.UpdateAsync(_newBlogPost);
            //ASSERT
            var refoundBlogPost = await _blogPostRepository.GetByIdAsync(_newBlogPost.Id);
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