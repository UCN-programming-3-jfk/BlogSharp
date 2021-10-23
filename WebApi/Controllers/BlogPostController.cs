using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Model;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {

        IBlogPostRepository _blogpostRepository;

        public BlogPostController(IBlogPostRepository blogpostRepository)
        {
            _blogpostRepository = blogpostRepository;
        }

        // GET: api/<BlogPostController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetAsync()
        {
            return  Ok(await _blogpostRepository.GetAllAsync());
        }

        // GET api/<BlogPostController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> Get(int id)
        {
            var blogPost = await _blogpostRepository.GetByIdAsync(id);
            if (blogPost == null) { return NotFound(); }
            else { return Ok(blogPost); }
        }

        // POST api/<BlogPostController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody]BlogPost newBlogPost)
        {
            return Ok(await _blogpostRepository.CreateAsync(newBlogPost));
        }

        // PUT api/<BlogPostController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]BlogPost blogPostToUpdate)
        {
            if (!await _blogpostRepository.UpdateAsync(blogPostToUpdate)) { return NotFound(); }
            else { return Ok(); }
        }

        // DELETE api/<BlogPostController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!await _blogpostRepository.DeleteAsync(id)) { return NotFound(); }
            else { return Ok(); }
        }
    }
}