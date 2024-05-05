using Blog_Management_System.Models;
using Blog_Management_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly IForumInteractive _forumInteractive;
        private readonly IUserInteractive _userInteractive;
        public HomeController(ILogger<HomeController> logger,
            IForumInteractive forumInteractive,
            IUserInteractive userInteractive,
            IHttpContextAccessor httpContextAccessor)
        {
            _forumInteractive = forumInteractive;
            _userInteractive = userInteractive;

            var username = httpContextAccessor?.HttpContext?.Session.GetString("Username");
            if (username is not null)
            {
                var user = _userInteractive.GetUserByUserName(username);
                _forumInteractive.User = user;
            }

            _forumInteractive.Forums = _forumInteractive.GetAllForums();
        }

        public IActionResult Index()
        {
            var forums = _forumInteractive.Forums;

            if (_forumInteractive.User is null)
            {
                var viewmodels = new HomeViewModels(forums, null);
                return View(viewmodels);
            }
            else
            {
                var viewmodels = new HomeViewModels(forums, _forumInteractive.User);
                return View(viewmodels);
            }
        }

        [HttpPost]
        public IActionResult Login(string? username)
        {
            if (username is not null)
            {
                _forumInteractive.User = new User();
                var user = _userInteractive.GetUserByUserName(username);
                if (user is not null)
                {
                    _forumInteractive.User = user;
                }
                else
                {
                    user = _userInteractive.CreateUser(username);
                    _forumInteractive.User = user;
                }

                HttpContext.Session.SetString("Username", user!.Username);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult CreateForum()
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
