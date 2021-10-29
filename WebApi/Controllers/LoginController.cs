using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        #region Repository and constructor
        IAuthorRepository _authorRepository;

        public LoginController(IConfiguration configuration) =>
            _authorRepository = new AuthorRepository(configuration.GetConnectionString("DefaultConnection"));
        #endregion

        // POST api/<LoginController>
        [HttpPost] 
        public async Task<ActionResult<int>> Post([FromBody] LoginDto loginvalues) =>
            await _authorRepository.LoginAsync(loginvalues.Email, loginvalues.Password);
    }
}