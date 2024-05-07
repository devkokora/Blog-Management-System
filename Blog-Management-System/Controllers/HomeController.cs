using Blog_Management_System.Models;
using Blog_Management_System.Models.Interactives;
using Blog_Management_System.Models.Tags;
using Blog_Management_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;

namespace Blog_Management_System.Controllers;

public class HomeController : Controller
{
    private readonly IForumInteractive _forumInteractive;
    private readonly IUserInteractive _userInteractive;
    private readonly ICategoryInteractive _categoryInteractive;
    private readonly IStatusInteractive _statusInteractive;
    public HomeController(ILogger<HomeController> logger,
        IForumInteractive forumInteractive,
        IUserInteractive userInteractive,
        ICategoryInteractive categoryInteractive,
        IStatusInteractive statusInteractive,
        IHttpContextAccessor httpContextAccessor)
    {
        _forumInteractive = forumInteractive;
        _userInteractive = userInteractive;
        _categoryInteractive = categoryInteractive;
        _statusInteractive = statusInteractive;

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
        var forums = _forumInteractive.Forums?.OrderByDescending(i => i.ForumId).ToList();

        if (forums is not null)
        {
            _statusInteractive.UpdateStatus(forums);
            var hotForums = _statusInteractive.HotStatus;
            var hotStatus = _statusInteractive.Statuses[0];
            var newForums = _statusInteractive.NewStatus;
            var newStatus = _statusInteractive.Statuses[1];

            foreach (var forum in forums)
            {
                if (newForums is not null)
                    UpdateForumStatus(newForums, newStatus, forum);
                if (hotForums is not null)
                    UpdateForumStatus(hotForums, hotStatus, forum);
                if (forum.CategoriesId is not null)
                    UpdateForumCategory(forum);
            }
        }

        if (_forumInteractive.User is null)
        {
            var viewmodels = new HomeViewModels(forums, _userInteractive.GetAllUser(), null, null);
            return View(viewmodels);
        }
        else
        {
            var viewmodels = new HomeViewModels(forums, _userInteractive.GetAllUser(), _forumInteractive.User, null);
            return View(viewmodels);
        }
    }

    private void UpdateForumStatus(List<Forum> filterForums, Status statusType, Forum forum)
    {
        if (filterForums.Any(f => f.ForumId == forum.ForumId))
        {
            if (forum.Statuses is null)
                forum.Statuses = [statusType];
            else if (!forum.Statuses.Contains(statusType))
                forum.Statuses.Add(statusType);
        }
        else
        {
            if (forum.Statuses is not null
                && forum.Statuses.Contains(statusType))
            {
                forum.Statuses.Remove(statusType);
            }
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
        if (_forumInteractive.User is null)
        {
            var viewmodels = new HomeViewModels(null, null, null, null);
            return View(viewmodels);
        }
        else
        {
            var viewmodels = new HomeViewModels(null, _userInteractive.GetAllUser(), _forumInteractive.User, null);
            return View(viewmodels);
        }
    }

    [HttpPost]
    public IActionResult CreateForum(Forum forum)
    {
        if (_forumInteractive.User is null)
            return View();
        else
        {
            forum.User = _forumInteractive.User;

            if (forum.CategoriesId is not null)
            {
                UpdateForumCategory(forum);
            }
            forum.Created_at = DateTime.Now;
            _forumInteractive.CreateForum(forum);
        }
        return RedirectToAction("Index");
    }

    private void UpdateForumCategory(Forum forum)
    {
        forum.Categories = [];
        foreach (var categoryId in forum.CategoriesId)
        {
            forum.Categories.Add(_categoryInteractive.Categories
                .First(c => c.Id == categoryId));
        }
    }

    public IActionResult EditForum(int? Id)
    {
        if (Id is null || Id == 0 || _forumInteractive.Forums is null)
            return NotFound();

        var forum = _forumInteractive.Forums.FirstOrDefault(s => s.ForumId == Id);
        if (forum is null)
            return NotFound();

        var viewmodels = new HomeViewModels(null, null, _forumInteractive.User, forum);

        return View(viewmodels);
    }

    [HttpPost]
    public IActionResult EditForum(Forum forum)
    {
        if (ModelState.IsValid)
        {
            if (forum.CategoriesId is not null)
            {
                forum.Categories ??= [];
                foreach (var categoryId in forum.CategoriesId)
                {
                    forum.Categories.Add(_categoryInteractive.Categories
                        .First(c => c.Id == categoryId));
                }
            }

            var temp = new Forum
            {
                Title = forum.Title,
                Body = forum.Body,
                Like = forum.Like,
                Created_at = DateTime.Now,
                CategoriesId = forum.CategoriesId ??= [],
                Categories = forum.Categories ??= [],
                Statuses = forum.Statuses ??= [],
                Comments = forum.Comments ??= [],
                UserId = forum.UserId,
            };
            _forumInteractive.RemoveForum(forum.ForumId);
            _forumInteractive.CreateForum(temp);
            return RedirectToAction("Index");
        }
        return View(forum);
    }

    public IActionResult DeleteForum(int? Id)
    {
        if (Id is not null)
        {
            _forumInteractive.RemoveForum(Id);
            return RedirectToAction("Index");
        }
        return NotFound();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
