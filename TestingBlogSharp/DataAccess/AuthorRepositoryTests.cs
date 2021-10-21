using DataAccess.Repositories;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Model;
using System.Collections.Generic;

namespace TestingBlogSharp.DataAccess
{
    public class AuthorRepositoryTests
    {
        private List<int> _createdAuthorIds = new();

        [TearDown]
        public void CleanUp()
        {
           
            Parallel.ForEach(_createdAuthorIds, async (id) =>
            {
                await new AuthorRepository(Configuration.CONNECTION_STRING).DeleteAsync(id);
                _createdAuthorIds.Remove(id);
            });
        }

        [Test]
        public async Task GetAuthorsAsync()
        {
            //ARRANGE
            var authorRep = new AuthorRepository(Configuration.CONNECTION_STRING);
            var newAuthor = new Author() { Email = "Test@testerson.com", PasswordHash = "98765" };
            var newAuthorId = await authorRep.CreateAsync(newAuthor);
            _createdAuthorIds.Add(newAuthorId);
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
            _createdAuthorIds.Add(newAuthorId);
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
            _createdAuthorIds.Add(newId);
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
            _createdAuthorIds.Add(newAuthor.Id);
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
            _createdAuthorIds.Add(newAuthor.Id);
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