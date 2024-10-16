using DataAccess.DaoClasses;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Model;
using System;

namespace TestingBlogSharp.DataAccess
{
    public class BlogPostDAOTests
    {
        //TODO: add test to test cascading delete of BlogPosts with their Author
        
        private AuthorDAO _authorRepository;
        private BlogPostDAO _blogPostRepository;
        private Author _newAuthor;
        private BlogPost _newBlogPost;

        [SetUp]
        public async Task SetupAsync()
        {
            _authorRepository = new AuthorDAO(Configuration.CONNECTION_STRING);
            _blogPostRepository= new BlogPostDAO(Configuration.CONNECTION_STRING);
            _newAuthor = new Author() { Email = "mail@myblog.com", BlogTitle = "Title of my blog" };
            _newAuthor.Id = await _authorRepository.CreateAsync(_newAuthor, "test1234");
            _newBlogPost = new BlogPost() { PostTitle = "My post title", AuthorId = _newAuthor.Id, PostContent = "Content", PostCreationDate = System.DateTime.Now };
            _newBlogPost.Id = await _blogPostRepository.CreateAsync(_newBlogPost);
        }

        [TearDown]
        public async Task CleanUpAsync()
        {
            await new AuthorDAO(Configuration.CONNECTION_STRING).DeleteAsync(_newAuthor.Id);
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
                var blogPost = new BlogPost() {AuthorId = _newAuthor.Id, PostCreationDate = DateTime.Now.AddDays(-blogPostCounter), PostContent= @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla elit libero, blandit a interdum nec, congue at tellus. Donec imperdiet risus in cursus consectetur. Vivamus sodales, orci vitae hendrerit convallis, mauris nunc tempor lacus, eget dictum mauris purus ac lorem. Aenean rhoncus lorem vitae eleifend viverra. Nullam ornare fringilla commodo. Vivamus felis nisl, rhoncus eu ipsum at, faucibus facilisis risus. Nam mattis arcu eget ante cursus, eu iaculis turpis finibus. Donec eget tellus quis magna tempor auctor non sed libero. Pellentesque convallis, erat eget finibus auctor, velit lacus ornare felis, vitae scelerisque justo justo eget massa. Praesent porttitor convallis lectus, ac tristique ipsum pretium id. Nam egestas tellus sapien, tincidunt consectetur ligula pretium ac.

Maecenas vitae congue velit,
                    nec eleifend leo.Praesent non ornare nunc.Interdum et malesuada fames ac ante ipsum primis in faucibus.Cras scelerisque risus eu arcu sollicitudin,
                    eget varius magna aliquam.Aliquam commodo turpis sed tortor ultrices,
                    eget tempus diam pellentesque.Suspendisse potenti.Maecenas aliquet justo in ornare gravida.Etiam vestibulum dignissim blandit.Integer feugiat sapien et purus vulputate maximus.Etiam finibus non orci quis commodo.In pretium sollicitudin urna,
                    at suscipit mi vulputate vel.Phasellus luctus orci nec orci aliquam blandit.Pellentesque id diam sit amet orci tempor facilisis euismod sed enim.Nunc finibus auctor eros id tempus.

Duis vitae tellus id sem gravida tincidunt ut ut massa.Suspendisse ut placerat dolor,
                    id hendrerit turpis.Vestibulum hendrerit,
                    sapien ut tincidunt iaculis,
                    est felis semper diam,
                    non tristique metus mi quis est.Fusce a mi fermentum,
                    fringilla leo ut,
                    ullamcorper justo.Integer non magna tortor.Donec et mauris vitae ante sollicitudin porta.Fusce cursus molestie neque,
                    a consequat lacus ullamcorper non.Nulla tortor est,
                    pulvinar quis faucibus at,
                    posuere non enim.Quisque semper felis sed turpis viverra,
                    sed fringilla mauris sollicitudin.Vivamus venenatis tortor augue,
                    pharetra rhoncus quam sollicitudin ultrices.

Vestibulum eu bibendum est,
                    at dapibus nisi.Phasellus placerat est purus,
                    nec facilisis odio feugiat in. Praesent hendrerit tempor turpis id accumsan.Sed sit amet eros leo.Aenean ultricies pellentesque mattis.Donec hendrerit erat eu urna sollicitudin tincidunt.Nullam volutpat feugiat nisi,
                    vel varius erat gravida a.Donec commodo ligula id sagittis congue.Nulla arcu turpis,
                    aliquet quis euismod a,
                    scelerisque non leo.Fusce pretium,
                    justo et lobortis venenatis,
                    metus neque interdum nisi,
                    vitae auctor arcu augue id arcu.Morbi suscipit nisi ac ultrices vehicula.In sed faucibus nunc,
                    sit amet ultricies enim.Nullam commodo dignissim mattis.

Donec vehicula non elit ut luctus.In tellus turpis,
                    lacinia at metus id,
                    condimentum dictum magna.Cras interdum lobortis erat,
                    at egestas dolor varius in. Sed elit nulla,
                    feugiat et lorem nec,
                    convallis pretium sem.Cras iaculis,
                    tortor eget consectetur iaculis,
                    ligula lectus consectetur mauris,
                    id aliquam augue nibh non augue.In et velit et justo euismod interdum.Phasellus vitae orci enim.Pellentesque sodales elit egestas tellus vehicula mollis.Nam et bibendum nunc.Quisque aliquet volutpat tellus.Maecenas risus nunc,
                    rutrum sodales sollicitudin sit amet,
                    sagittis vel odio.Vestibulum commodo magna et tellus scelerisque,
                    a fermentum neque gravida.", PostTitle=$"Great title {blogPostCounter}" };
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