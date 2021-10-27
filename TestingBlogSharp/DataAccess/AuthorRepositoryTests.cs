using DataAccess.Repositories;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Model;

namespace TestingBlogSharp.DataAccess
{

    //TODO: add test to test cascading delete of BlogPosts with their Author
    public class AuthorRepositoryTests
    {
        private int _createdAuthorId;

        [TearDown]
        public void CleanUp()
        {
            //TearDown methods cannot run async :(
            Task.Run(() => new AuthorRepository(Configuration.CONNECTION_STRING).DeleteAsync(_createdAuthorId)).Wait();
            
        }

        [Test]
        public async Task CreateAuthorAsync()
        {
            //ARRANGE
            var authorRep = new AuthorRepository(Configuration.CONNECTION_STRING);
            var newAuthor = new Author() { Email="Test@testerson.com", BlogTitle = "A grand title" };
            var password = "SuperSecretPassword12345678!";
            //ACT
            var newAuthorId = await authorRep.CreateAsync(newAuthor, password);
            _createdAuthorId = newAuthorId;
            //ASSERT
            Assert.IsTrue(newAuthorId > 0, "Created author ID not returned");
        }

        [Test]
        public async Task GetAuthorsAsync()
        {
            //ARRANGE
            var authorRep = new AuthorRepository(Configuration.CONNECTION_STRING);
            var newAuthor = new Author() { Email = "Test@testerson.com", BlogTitle = "A grand title" };
            var password = "SuperSecretPassword12345678!";
            var newAuthorId = await authorRep.CreateAsync(newAuthor, password);
            _createdAuthorId = newAuthorId;
            //ACT
            var authors = await authorRep.GetAllAsync();
            //ASSERT
            Assert.IsTrue(authors.Count() > 0, "No authors returned");
        }

        [Test]
        public async Task DeleteAuthorAsync()
        {
            //ARRANGE
            var authorRep = new AuthorRepository(Configuration.CONNECTION_STRING);
            var newAuthor = new Author() { Email = "Test@testerson.com", BlogTitle = "A grand title" };
            var newAuthorId = await authorRep.CreateAsync(newAuthor, "test");
            _createdAuthorId = newAuthorId;
            //ACT
            bool deleted = await authorRep.DeleteAsync(newAuthorId);
            //ASSERT
            Assert.IsTrue(deleted, "Author not deleted");
        }

        [Test]
        public async Task FindAuthorAsync()
        {
            //ARRANGE
            var authorRep = new AuthorRepository(Configuration.CONNECTION_STRING);
            var newAuthor = new Author() { Email = "Test@testerson.com", BlogTitle = "A grand title" };
            newAuthor.Id = await authorRep.CreateAsync(newAuthor, "test");
            _createdAuthorId = newAuthor.Id;
            //ACT
            var refoundAuthor = await authorRep.GetByIdAsync(newAuthor.Id);
            //ASSERT
            Assert.IsTrue(newAuthor.Id == refoundAuthor.Id && newAuthor.Email == refoundAuthor.Email && newAuthor.BlogTitle == refoundAuthor.BlogTitle, "Author not found again");
        }

        [Test]
        public async Task UpdateAuthorAsync()
        {
            //ARRANGE
            string updatedEmail = "Bing@Bingsby.test";
            var authorRep = new AuthorRepository(Configuration.CONNECTION_STRING);
            var newAuthor = new Author() { Email = "Test@testerson.com", BlogTitle = "A grand title" };
            newAuthor.Id = await authorRep.CreateAsync(newAuthor, "test");
            _createdAuthorId = newAuthor.Id;
            newAuthor.Email = updatedEmail;;
            //ACT
            await authorRep.UpdateAsync(newAuthor );
            //ASSERT
            var refoundAuthor = await authorRep.GetByIdAsync(newAuthor.Id);
            Assert.IsTrue(refoundAuthor.Email == updatedEmail, "Author not updated");
        }

        [Test]
        public async Task UpdateAuthorPasswordAndLoginAsync()
        {
            //ARRANGE
            var authorRep = new AuthorRepository(Configuration.CONNECTION_STRING);
            var newAuthor = new Author() { Email = "Test@testerson.com", BlogTitle = "A grand title" };
            var oldPassword = "TestOldPassword";
            var newPassword = "TestNewPassword";
            newAuthor.Id = await authorRep.CreateAsync(newAuthor, oldPassword);
            _createdAuthorId = newAuthor.Id;

            //ACT
            var updateSuccess = await authorRep.UpdatePasswordAsync(newAuthor.Email, oldPassword, newPassword);
            
            //ASSERT
            var loginWithNewPasswordOk = await authorRep.LoginAsync(newAuthor.Email, newPassword);
            Assert.IsTrue(updateSuccess, "Author not updated");
            Assert.IsTrue(loginWithNewPasswordOk > 0, "Unable to login with Author's updated password");
        }

    }
}