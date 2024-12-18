using DataAccess.DaoClasses;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Model;

namespace TestingBlogSharp.DataAccess
{

    
    public class AuthorDAOTests
    {
        private Author _newAuthor;
        private AuthorDAO _authorRepository;
        private string _password = "TestPassword";

        [SetUp]
        public async Task Setup()
        {
             _authorRepository = new AuthorDAO(Configuration.CONNECTION_STRING);
            await CreateNewAuthorAsync();
        }

        [TearDown]
        public async Task CleanUp()
        {
            await _authorRepository.DeleteAsync(_newAuthor.Id);
        }

        private async Task<Author> CreateNewAuthorAsync()
        {
            _newAuthor = new Author() { Email = "New author for post tests", BlogTitle = "Title of my blog" };
            _newAuthor.Id = await _authorRepository.CreateAsync(_newAuthor, _password);
            return _newAuthor;
        }

        [Test]
        public void CreateAuthor()
        {
            //ARRANGE & ACT is done in Setup()
            //ASSERT
            Assert.That(_newAuthor.Id > 0, "Created author ID not returned");
        }

        [Test]
        public async Task GetAuthorsAsync()
        {
            //ARRANGE
            //ACT
            var authors = await _authorRepository.GetAllAsync();
            //ASSERT
            Assert.That(authors.Count() > 0, "No authors returned");
        }

        [Test]
        public async Task DeleteAuthorAsync()
        {
            //ARRANGE is done in Setup()
            
            //ACT 
            bool deleted = await _authorRepository.DeleteAsync(_newAuthor.Id);
            //ASSERT
            Assert.That(deleted, "Author not deleted");
        }

        [Test]
        public async Task FindAuthorAsync()
        {
            //ARRANGE is done in Setup()

            //ACT
            var refoundAuthor = await _authorRepository.GetByIdAsync(_newAuthor.Id);
            //ASSERT
            Assert.That(_newAuthor.Id == refoundAuthor.Id && _newAuthor.Email == refoundAuthor.Email && _newAuthor.BlogTitle == refoundAuthor.BlogTitle, "Author not found again by id");
        }

        [Test]
        public async Task FindAuthorByEmailAsync()
        {
            //ARRANGE is done in Setup()

            //ACT
            var refoundAuthor = await _authorRepository.GetByEmailAsync(_newAuthor.Email);
            //ASSERT
            Assert.That(_newAuthor.Id == refoundAuthor.Id && _newAuthor.Email == refoundAuthor.Email && _newAuthor.BlogTitle == refoundAuthor.BlogTitle, "Author not found again by email");
        }

        [Test]
        public async Task UpdateAuthorAsync()
        {
            //ARRANGE
            string updatedEmail = "Bing@Bingsby.test";
            _newAuthor.Email = updatedEmail;
            //ACT
            await _authorRepository.UpdateAsync(_newAuthor);
            //ASSERT
            var refoundAuthor = await _authorRepository.GetByIdAsync(_newAuthor.Id);
            Assert.That(refoundAuthor.Email == updatedEmail, "Author not updated");
        }

        [Test]
        public async Task UpdateAuthorPasswordAndLoginAsync()
        {
            //ARRANGE
            var newPassword = "TestNewPassword";

            //ACT
            var updateSuccess = await _authorRepository.UpdatePasswordAsync(_newAuthor.Email, _password, newPassword);
            
            //ASSERT
            var loginWithNewPasswordOk = await _authorRepository.LoginAsync(_newAuthor.Email, newPassword);
            Assert.That(updateSuccess, "Author not updated");
            Assert.That(loginWithNewPasswordOk > 0, "Unable to login with Author's updated password");
        }

    }
}