using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Dynamic;
using System.Threading.Tasks;
using WebApiClient;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    /// <summary>
    /// This class implements the ASP.NET Core MVC controller which is mapped to the root URI.
    /// It provides the front page view (the 10 latest blogposts), and the error page, if something goes wrong.
    /// </summary>
    {
        private IBlogSharpApiClient _client;

        public HomeController(IBlogSharpApiClient client) => _client = client;

        public async Task<IActionResult> Index()
        {
            dynamic model = new ExpandoObject();
            model.BlogPosts = await _client.Get10LatestBlogPostsAsync();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}