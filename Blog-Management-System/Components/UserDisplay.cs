using Blog_Management_System.Models.Interactives;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Management_System.Components
{
    public class UserDisplay : ViewComponent
    {
        private readonly IUserInteractive _userInteractive;

        public UserDisplay(IUserInteractive userInteractive)
        {
            _userInteractive = userInteractive;
        }

        public IViewComponentResult Invoke()
        {
            var user = _userInteractive.User;
            return View(user);
            //Console.WriteLine("Hello 239843290482309");
            //return Content("this is test");
        }
    }
}
