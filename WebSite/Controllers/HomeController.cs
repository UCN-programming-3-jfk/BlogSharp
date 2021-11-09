using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Dynamic;
using System.Threading.Tasks;
using WebApiClient;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        private IBlogSharpApiClient _client;

        public HomeController(IBlogSharpApiClient client)
        {
            _client = client;
        }

public async Task<IActionResult> Index()
{
    dynamic model = new ExpandoObject();
    model.BlogPosts = await _client.Get10LatestBlogPostsAsync();
    return View(model);
}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
