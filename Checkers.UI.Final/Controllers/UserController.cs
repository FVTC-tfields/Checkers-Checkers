using BDF.Utility;
using System.Net.Http;

namespace Checkers.UI.Final.Controllers
{
    public class UserController : GenericController<User>
    {
        private ApiClient apiClient;

        public UserController(HttpClient httpClient) : base(httpClient)
        {
            this.apiClient = new ApiClient(httpClient.BaseAddress.AbsoluteUri);

        }

        private void SetUser(User user)
        {
            HttpContext.Session.SetObject("user", user);

            if (user != null)
            {
                HttpContext.Session.SetObject("fullname", "Welcome " + user.FullName);
                HttpContext.Session.SetObject("userID", user.Id);
            }
            else
            {
                HttpContext.Session.SetObject("fullname", string.Empty);
                HttpContext.Session.SetObject("userID", string.Empty);
            }
        }

        public IActionResult Logout()
        {
            ViewBag.Title = "Logout";
            SetUser(null);
            return View();
        }

        public IActionResult Login(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;
            ViewBag.Title = "Login";
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            try
            {
                if (apiClient.Authenticate(user.UserName, user.Password) == System.Net.HttpStatusCode.OK)
                    SetUser(user);
                if (TempData["returnUrl"] != null)
                    return Redirect(TempData["returnUrl"]?.ToString());
                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Login";
                ViewBag.Error = ex.Message;
                return View(user);
            }
        }
    }
}
