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
        private AuthorDto _newAuthorDto;
        private BlogSharpApiClient _client = new BlogSharpApiClient(Configuration.WEB_API_URL);

        private async Task CreateNewAuthorAsync()
        {
            _newAuthorDto = new AuthorDto() { Email = "TestingBlogSharp.WebApi@myblog.com", BlogTitle = "Title of my blog", Password = "password" };
           _newAuthorDto.Id = await _client.CreateAuthorAsync(_newAuthorDto);
        }

        [SetUp]
        public async Task Setup()
        {
            await CreateNewAuthorAsync();
        }

        [TearDown]
        public void CleanUp()
        {
            try
            {
                Task.Run(() => _client.DeleteAuthorAsync(_newAuthorDto.Id)).Wait();
            }
            catch (System.Exception ex)
            {
            }
        }

        [Test]
        public async Task CreateAuthorAsync()
        {
            //ARRANGE
            //ACT
            
            
            //ASSERT
            Assert.IsTrue(_newAuthorDto.Id > 0, "Created author ID not returned");
        }

        [Test]
        public async Task GetAuthorsAsync()
        {
            //ARRANGE
            //ACT
            var authors = await _client.GetAllAuthorsAsync();
            //ASSERT
            Assert.IsTrue(authors.Count() > 0, "No authors returned");
        }

        [Test]
        public void DeleteAuthor()
        {
            //ARRANGE
            //ACT
            bool deleted = Task.Run(() =>_client.DeleteAuthorAsync(_newAuthorDto.Id)).Result;
            //ASSERT
            Assert.IsTrue(deleted, "Author not deleted");
        }

        [Test]
        public async Task FindAuthorAsync()
        {
            //ARRANGE
            //ACT
            var refoundAuthor = await _client.GetAuthorByIdAsync(_newAuthorDto.Id);
            
            //ASSERT
            Assert.IsTrue(_newAuthorDto.Id == refoundAuthor.Id && _newAuthorDto.Email == refoundAuthor.Email && _newAuthorDto.BlogTitle == refoundAuthor.BlogTitle, "Author not found again");
        }

        [Test]
        public async Task UpdateAuthorAsync()
        {
            //ARRANGE
            string updatedEmail = "Bing@Bingsby.test";
            _newAuthorDto.Email = updatedEmail; ;
            //ACT
            await _client.UpdateAuthorAsync(_newAuthorDto);
            //ASSERT
            var refoundAuthor = await _client.GetAuthorByIdAsync(_newAuthorDto.Id);
            Assert.IsTrue(refoundAuthor.Email == updatedEmail, "Author not updated");
        }

        [Test]
        public async Task UpdateAuthorPasswordAsync()
        {
            //ARRANGE
            _newAuthorDto.NewPassword = "NewPassword1234!";
            //ACT
            var updateSuccess = await _client.UpdateAuthorPasswordAsync(_newAuthorDto);

            //ASSERT
            _newAuthorDto.Password = _newAuthorDto.NewPassword;
            var loginWithNewPasswordOk = await _client.LoginAsync(_newAuthorDto);
            Assert.IsTrue(updateSuccess, "Author not updated");
            Assert.IsTrue(loginWithNewPasswordOk > 0, "Unable to login with Author's updated password");
        }
    }
}