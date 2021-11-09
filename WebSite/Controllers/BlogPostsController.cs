using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using WebApiClient;

namespace WebSite.Controllers
{
    public class BlogPostsController : Controller
    {

        private IBlogSharpApiClient _client;

        public BlogPostsController(IBlogSharpApiClient client)
        {
            _client = client;
        }
        // GET: BlogPostController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BlogPostController/Details/5
public async Task<ActionResult> Details(int id)
{
    var blogPost = await _client.GetBlogPostByIdAsync(id);
    var author = await _client.GetAuthorByIdAsync(blogPost.AuthorId);
    dynamic model = new ExpandoObject();
    model.BlogPost = blogPost;
    model.Author = author;
    return View(model);
}

        // GET: BlogPostController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogPostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            //First get user claims    
            //var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            //Filter specific claim    
            //claims?.FirstOrDefault(x => x.Type.Equals("UserName", StringComparison.OrdinalIgnoreCase))?.Value
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BlogPostController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BlogPostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BlogPostController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BlogPostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
