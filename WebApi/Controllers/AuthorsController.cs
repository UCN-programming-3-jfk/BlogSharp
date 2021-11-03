using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Model;
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

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        #endregion

        #region Default CRUD actions
        // GET: api/authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAsync([FromQuery] string email)
        {
            IEnumerable<Author> authors = null;
            if (!string.IsNullOrEmpty(email)) { authors = new List<Author>() { await _authorRepository.GetByEmailAsync(email) }; }
            else { authors = await _authorRepository.GetAllAsync(); }
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
        public async Task<ActionResult<int>> Post([FromBody] AuthorDto newAuthorDto)
        {
            return Ok(await _authorRepository.CreateAsync(newAuthorDto.FromDto(), newAuthorDto.Password));
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
        public async Task<ActionResult> UpdatePassword(int id, [FromBody] AuthorDto passwordUpdateInfo)
        {
            if (!await _authorRepository.UpdatePasswordAsync(passwordUpdateInfo.Email, passwordUpdateInfo.Password, passwordUpdateInfo.NewPassword))
            { return NotFound(); }
            { return Ok(); }
        } 
        #endregion
    }
}