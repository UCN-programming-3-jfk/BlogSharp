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
    public class BlogPostsController : ControllerBase
    {

        #region Repository and constructor
        IBlogPostRepository _blogpostRepository;

        public BlogPostsController(IConfiguration configuration)
        {
            _blogpostRepository = new BlogPostRepository(configuration.GetConnectionString("DefaultConnection"));
        }
        #endregion

        #region Default CRUD actions
        // GET: api/<BlogPostController>?authorid=23
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPostDto>>> Get([FromQuery] int? authorId, [FromQuery] string filter)
        {

            IEnumerable<BlogPost> posts;

            if (authorId.HasValue)
            {
                posts = await _blogpostRepository.GetByAuthorIdAsync(authorId.Value);
            }
            if (!string.IsNullOrEmpty(filter) && filter == "getlatest10")
            {
                posts = await _blogpostRepository.Get10LatestAsync();
            }
            else
            {
                posts = await _blogpostRepository.GetAllAsync();
            }

            return Ok(posts.ToDtos());
        }

        // GET api/<BlogPostController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPostDto>> Get(int id)
        {
            var blogPost = await _blogpostRepository.GetByIdAsync(id);
            if (blogPost == null) { return NotFound(); }
            else { return Ok(blogPost); }
        }

        // POST api/<BlogPostController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] BlogPostDto newBlogPostDto)
        {
            return Ok(await _blogpostRepository.CreateAsync(newBlogPostDto.FromDto()));
        }

        // PUT api/<BlogPostController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] BlogPostDto blogPostDtoToUpdate)
        {
            if (!await _blogpostRepository.UpdateAsync(blogPostDtoToUpdate.FromDto())) { return NotFound(); }
            else { return Ok(); }
        }

        // DELETE api/<BlogPostController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!await _blogpostRepository.DeleteAsync(id)) { return NotFound(); }
            else { return Ok(); }
        } 
        #endregion
    }
}