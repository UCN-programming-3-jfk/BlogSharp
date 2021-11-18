using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.DTOs;

namespace WebSite.Controllers
{

    /// <summary>
    /// This class implements the ASP.NET Core MVC controller which is mapped to the "/Authors" URI.
    /// It provides access to CRUD functionality for the AuthorDTO.
    /// Notice the [Authorize] attribute on the Create, Edit and Delete actions, which demands the user is authenticated
    /// and will redirect to "/accounts/login" if necessary.
    /// </summary>


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

        // GET: BlogPostsController/Create
        [Authorize]
        public ActionResult Create() => View();

        // POST: BlogPostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create(BlogPostDto newBlogPost)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            string userIdValue = claims.Where(c => c.Type == "user_id").FirstOrDefault()?.Value;

            newBlogPost.AuthorId = int.Parse(userIdValue);
            newBlogPost.PostCreationDate = DateTime.Now;
            try
            {
                newBlogPost.Id = await _client.CreateBlogPostAsync(newBlogPost);
                TempData["Message"] = "Blog post created";
                return RedirectToAction(nameof(Details), new { Id = newBlogPost.Id });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: BlogPostController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BlogPostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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
        [Authorize]
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
