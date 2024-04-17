using Microsoft.AspNetCore.Mvc;

namespace Checkers.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
