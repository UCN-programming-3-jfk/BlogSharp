using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.DTOs;

namespace WebSite.Controllers;
/// <summary>
/// This class implements the ASP.NET Core MVC controller which is mapped to the "/Authors" URI.
/// It provides access to CRUD functionality for the AuthorDTO.
/// Notice the [Authorize] attribute on the Edit and Delete actions, which demands the user is authenticated
/// and will redirect to "/accounts/login" if necessary.
/// </summary>

public class AuthorsController : Controller
{
    private IBlogSharpApiClient _client;
    public AuthorsController(IBlogSharpApiClient client) => _client = client;

    // GET: AuthorController
    [HttpGet("Authors/{authorId}/BlogPosts")]
    public async Task<ActionResult> BlogPosts(int authorId)
    {
        var author = await _client.GetAuthorByIdAsync(authorId);
        var blogPosts = await _client.GetBlogPostsFromAuthorIdAsync(authorId);
        var model = new { Author = author, BlogPosts = blogPosts };
        return View(model);
    }


    // GET: AuthorController
    public ActionResult Index() => View();

    // GET: AuthorController/Details/5
    public ActionResult Details(int id) => View();

    // GET: AuthorController/Create
    public ActionResult Create() => View();

    // POST: AuthorController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(AuthorDto author)
    {
        ModelState.Remove("NewPassword");
        if (!ModelState.IsValid)
        {
            ViewBag.ErrorMessage = "User not created - error in submitted data!";
        }
        else
        {
            try
            {
                if (await _client.CreateAuthorAsync(author) > 0)
                {
                    TempData["Message"] = $"User {author.Email} created!";
                    return RedirectToAction(nameof(Index), "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "User not created!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
        }
        return View();
    }

    // GET: AuthorController/Edit/5
    [Authorize(Roles = "Author")]
    public ActionResult Edit(int id) => View();

    // POST: AuthorController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Author")]
    public ActionResult Edit(int id, AuthorDto author)
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

    // GET: AuthorController/Delete/5
    [Authorize(Roles = "Author")]
    public ActionResult Delete(int id) => View();

    // POST: AuthorController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Author")]
    public ActionResult Delete(int id, AuthorDto author)
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
