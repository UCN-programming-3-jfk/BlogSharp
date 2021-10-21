using DataAccess.Repositories;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Model;

namespace TestingBlogSharp.DataAccess
{
    public class AuthorRepositoryTests
    {
        [Test]
        public async Task GetAuthorsAsync()
        {
            //ARRANGE
            var authorRep = new AuthorRepository(Configuration.CONNECTION_STRING);
            //ACT
            var authors = await authorRep.GetAllAsync();
            //ASSERT
            Assert.IsTrue(authors.Count() > 0, "No authors returned");
        }

        [Test]
        public async Task CreateAuthorAsync()
        {
            //ARRANGE
            var authorRep = new AuthorRepository(Configuration.CONNECTION_STRING);
            var newAuthor = new Author() { Email="Test@testerson.com", PasswordHash="98765"};
            //ACT
            var newAuthorId = await authorRep.CreateAsync(newAuthor);
            //ASSERT
            Assert.IsTrue(newAuthorId > 0, "Created author ID not returned");
        }

        [Test]
        public async Task DeleteAuthorAsync()
        {
            //ARRANGE
            var authorRep = new AuthorRepository(Configuration.CONNECTION_STRING);
            var newAuthor = new Author() { Email = "Test@testerson.com", PasswordHash = "98765" };
            var newId = await authorRep.CreateAsync(newAuthor);
            //ACT
            bool deleted = await authorRep.DeleteAsync(newId);
            //ASSERT
            Assert.IsTrue(deleted, "Author not deleted");
        }

        [Test]
        public async Task FindAuthorAsync()
        {
            //ARRANGE
            var authorRep = new AuthorRepository(Configuration.CONNECTION_STRING);
            var newAuthor = new Author() { Email = "Test@testerson.com", PasswordHash = "98765" };
            newAuthor.Id = await authorRep.CreateAsync(newAuthor);
            //ACT
            var refoundAuthor = await authorRep.GetByIdAsync(newAuthor.Id);
            //ASSERT
            Assert.IsTrue(newAuthor.Id == refoundAuthor.Id && newAuthor.Email == refoundAuthor.Email && newAuthor.PasswordHash == refoundAuthor.PasswordHash, "Author not found again");
        }

        [Test]
        public async Task UpdateAuthorAsync()
        {
            //ARRANGE
            string updatedEmail = "Bing@Bingsby.test";
            string updatedPasswordHash = "123456";
            var authorRep = new AuthorRepository(Configuration.CONNECTION_STRING);
            var newAuthor = new Author() { Email = "Test@testerson.com", PasswordHash = "98765" };
            newAuthor.Id = await authorRep.CreateAsync(newAuthor);
            newAuthor.Email = updatedEmail;
            newAuthor.PasswordHash = updatedPasswordHash;
            //ACT
            await authorRep.UpdateAsync(newAuthor);
            //ASSERT
            var refoundAuthor = await authorRep.GetByIdAsync(newAuthor.Id);
            Assert.IsTrue(refoundAuthor.Email == updatedEmail && refoundAuthor.PasswordHash == updatedPasswordHash, "Author not updated");
        }
    }
}