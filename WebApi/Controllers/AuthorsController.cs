using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Model;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {

        IAuthorRepository _authorRepository;

        public AuthorsController(IConfiguration configuration)
        {
            _authorRepository = new AuthorRepository(configuration.GetConnectionString("DefaultConnection"));
        }

        // GET: api/<AuthorController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAsync()
        {
            return  Ok(await _authorRepository.GetAllAsync());
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> Get(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) { return NotFound(); }
            else { return Ok(author); }
        }

        // POST api/<AuthorController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody]Author newAuthor, string password)
        {
            return Ok(await _authorRepository.CreateAsync(newAuthor, password));
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]Author authorToUpdate)
        {
            if (!await _authorRepository.UpdateAsync(authorToUpdate)) { return NotFound(); }
            else { return Ok(); }
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!await _authorRepository.DeleteAsync(id)) { return NotFound(); }
            else { return Ok(); }
        }
    }
}