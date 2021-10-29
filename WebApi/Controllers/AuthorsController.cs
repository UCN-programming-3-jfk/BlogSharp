using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.DTOs;
using WebApi.DTOs.Converters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {

        #region Repository and constructor
        IAuthorRepository _authorRepository;

        public AuthorsController(IConfiguration configuration)
        {
            _authorRepository = new AuthorRepository(configuration.GetConnectionString("DefaultConnection"));
        }
        #endregion

        #region Default CRUD actions
        // GET: api/authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            return Ok(authors.ToDtos());
        }

        // GET api/authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) { return NotFound(); }
            else { return Ok(author.ToDto()); }
        }

        // POST api/authors
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] AuthorDto newAuthorDto, string password)
        {
            return Ok(await _authorRepository.CreateAsync(newAuthorDto.FromDto(), password));
        }

        // PUT api/authors/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AuthorDto authorDtoToUpdate)
        {
            if (!await _authorRepository.UpdateAsync(authorDtoToUpdate.FromDto())) { return NotFound(); }
            { return Ok(); }
        }



        // DELETE api/authors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!await _authorRepository.DeleteAsync(id)) { return NotFound(); }
            else { return Ok(); }
        }

        #endregion

        #region Password update action

        [HttpPut("{id}/Password")]
        public async Task<ActionResult> UpdatePassword(int id, [FromBody] PasswordUpdateDto passwordUpdateInfo)
        {
            if (!await _authorRepository.UpdatePasswordAsync(passwordUpdateInfo.Email, passwordUpdateInfo.OldPassword, passwordUpdateInfo.NewPassword))
            { return NotFound(); }
            { return Ok(); }
        } 
        #endregion
    }
}