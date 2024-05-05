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
            IUserInteractive userInteractive)
        {
            _forumInteractive = forumInteractive;
            _userInteractive = userInteractive;
            _forumInteractive.Forums = _forumInteractive.GetAllForums();
        }

        public IActionResult Index()
        {
            var forums = _forumInteractive.Forums;
            var user = _forumInteractive.User;
            var viewmodels = new HomeViewModels(forums, user);
            return View(viewmodels);
        }

        public IActionResult Login(string name)
        {
            var user = _userInteractive.GetUserByUserName(name);
            if (user is not null)
            {
                _forumInteractive.User = user;
            }
            else
            {
                user = _userInteractive.CreateUser(name);
                _forumInteractive.User = user;
            }
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
