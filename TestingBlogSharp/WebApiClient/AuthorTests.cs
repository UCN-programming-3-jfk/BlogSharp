using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.DTOs;

namespace TestingBlogSharp.WebApi
{

    //TODO: add test to test cascading delete of BlogPosts with their Author
    public class AuthorTests
    {
        private int _createdAuthorId;

        [TearDown]
        public async Task CleanUp()
        {
            //TearDown methods cannot run async :(
            await new BlogSharpApiClient(Configuration.WEB_API_URL).DeleteAuthorAsync(_createdAuthorId);
        }

        [Test]
        public async Task CreateAuthorAsync()
        {
            //ARRANGE
            var authorRep = new BlogSharpApiClient(Configuration.WEB_API_URL);
            var newAuthorDto = new AuthorDto() { Email = "Test@testerson.com", BlogTitle = "A grand title" };
            var password = "SuperSecretPassword12345678!";
            //ACT
            _createdAuthorId = await authorRep.CreateAuthorAsync(newAuthorDto, password);
            
            //ASSERT
            Assert.IsTrue(_createdAuthorId > 0, "Created author ID not returned");
        }

        [Test]
        public async Task GetAuthorsAsync()
        {
            //ARRANGE
            var authorRep = new BlogSharpApiClient(Configuration.WEB_API_URL);
            var newAuthorDto = new AuthorDto() { Email = "Test@testerson.com", BlogTitle = "A grand title" };
            var password = "SuperSecretPassword12345678!";
            var newAuthorDtoId = await authorRep.CreateAuthorAsync(newAuthorDto, password);
            _createdAuthorId = newAuthorDtoId;
            //ACT
            var authors = await authorRep.GetAllAuthorsAsync();
            //ASSERT
            Assert.IsTrue(authors.Count() > 0, "No authors returned");
        }

        [Test]
        public async Task DeleteAuthorAsync()
        {
            //ARRANGE
            var authorRep = new BlogSharpApiClient(Configuration.WEB_API_URL);
            var newAuthorDto = new AuthorDto() { Email = "Test@testerson.com", BlogTitle = "A grand title" };
            var newAuthorDtoId = await authorRep.CreateAuthorAsync(newAuthorDto, "test");
            _createdAuthorId = newAuthorDtoId;
            //ACT
            bool deleted = await authorRep.DeleteAuthorAsync(newAuthorDtoId);
            //ASSERT
            Assert.IsTrue(deleted, "Author not deleted");
        }

        [Test]
        public async Task FindAuthorAsync()
        {
            //ARRANGE
            var authorRep = new BlogSharpApiClient(Configuration.WEB_API_URL);
            var newAuthorDto = new AuthorDto() { Email = "Test@testerson.com", BlogTitle = "A grand title" };
            newAuthorDto.Id = await authorRep.CreateAuthorAsync(newAuthorDto, "test");
            _createdAuthorId = newAuthorDto.Id;

            //ACT
            var refoundAuthor = await authorRep.GetAuthorByIdAsync(newAuthorDto.Id);
            
            //ASSERT
            Assert.IsTrue(newAuthorDto.Id == refoundAuthor.Id && newAuthorDto.Email == refoundAuthor.Email && newAuthorDto.BlogTitle == refoundAuthor.BlogTitle, "Author not found again");
        }

        [Test]
        public async Task UpdateAuthorAsync()
        {
            //ARRANGE
            string updatedEmail = "Bing@Bingsby.test";
            var authorRep = new BlogSharpApiClient(Configuration.WEB_API_URL);
            var newAuthorDto = new AuthorDto() { Email = "Test@testerson.com", BlogTitle = "A grand title" };
            newAuthorDto.Id = await authorRep.CreateAuthorAsync(newAuthorDto, "test");
            _createdAuthorId = newAuthorDto.Id;
            newAuthorDto.Email = updatedEmail; ;
            //ACT
            await authorRep.UpdateAuthorAsync(newAuthorDto);
            //ASSERT
            var refoundAuthor = await authorRep.GetAuthorByIdAsync(newAuthorDto.Id);
            Assert.IsTrue(refoundAuthor.Email == updatedEmail, "Author not updated");
        }

        [Test]
        public async Task UpdateAuthorPasswordAndLoginAsync()
        {
            //ARRANGE
            var authorRep = new BlogSharpApiClient(Configuration.WEB_API_URL);
            var newAuthorDto = new AuthorDto() { Email = "Test@testerson.com", BlogTitle = "A grand title" };
            var oldPassword = "TestOldPassword";
            var newPassword = "TestNewPassword";
            newAuthorDto.Id = await authorRep.CreateAuthorAsync(newAuthorDto, oldPassword);
            _createdAuthorId = newAuthorDto.Id;

            //ACT
            var updateSuccess = await authorRep.UpdateAuthorPasswordAsync(newAuthorDto.Email, oldPassword, newPassword);

            //ASSERT
            var loginWithNewPasswordOk = await authorRep.LoginAsync(newAuthorDto.Email, newPassword);
            Assert.IsTrue(updateSuccess, "Author not updated");
            Assert.IsTrue(loginWithNewPasswordOk > 0, "Unable to login with Author's updated password");
        }
    }
}