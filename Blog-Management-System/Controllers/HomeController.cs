using Blog_Management_System.Models;
using Blog_Management_System.Models.Interactives;
using Blog_Management_System.Models.Tags;
using Blog_Management_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blog_Management_System.Controllers;

public class HomeController : Controller
{
    private readonly IForumInteractive _forumInteractive;
    private readonly IUserInteractive _userInteractive;
    private readonly ICategoryInteractive _categoryInteractive;
    private readonly IStatusInteractive _statusInteractive;
    private readonly ICommentInteractive _commentInteractive;

    public HomeController(ILogger<HomeController> logger,
        IForumInteractive forumInteractive,
        IUserInteractive userInteractive,
        ICategoryInteractive categoryInteractive,
        IStatusInteractive statusInteractive,
        ICommentInteractive commentInteractive,
        IHttpContextAccessor httpContextAccessor)
    {
        _forumInteractive = forumInteractive;
        _userInteractive = userInteractive;
        _categoryInteractive = categoryInteractive;
        _statusInteractive = statusInteractive;
        _commentInteractive = commentInteractive;

        var username = httpContextAccessor?.HttpContext?.Session.GetString("Username");
        if (username is not null)
        {
            var user = _userInteractive.GetUserByUserName(username);
            _userInteractive.User = user;
        }

        _forumInteractive.Forums = _forumInteractive.GetAllForums();
    }

    public IActionResult Index()
    {
        List<Forum>? forums = [];
        List<string>? tagForums = [];

        var forumsJson = TempData["tagFilters"] as string;

        if (!string.IsNullOrEmpty(forumsJson))
        {
            forums = GetFilterTagForums(JsonSerializer.Deserialize<List<string>>(forumsJson));
        }

        if (forums is null || forums.Count == 0)
            forums = _forumInteractive.Forums?.OrderByDescending(i => i.ForumId).ToList();

        InitialIndexForums(forums);

        if (_userInteractive.User is null)
        {
            var viewmodels = new HomeViewModels(forums, _userInteractive.GetAllUser(), null, null);
            return View(viewmodels);
        }
        else
        {
            var viewmodels = new HomeViewModels(forums, _userInteractive.GetAllUser(), _userInteractive.User, null);
            return View(viewmodels);
        }
    }

    private List<Forum>? GetFilterTagForums(List<string>? tagFilters)
    {
        if (tagFilters is null)
            return null;

        _categoryInteractive.UpdateCategories();
        _statusInteractive.UpdateStatus(_forumInteractive.Forums);

        var allTags = new List<ITagFilter>();
        allTags.AddRange(_categoryInteractive.Categories.ToList());
        allTags.AddRange(_statusInteractive.Statuses.ToList());

        var existingTags = new List<ITagFilter>();

        foreach (var tagFilter in tagFilters)
        {
            var temp = allTags.FirstOrDefault(at => at.Name == tagFilter);
            if (temp is not null)
                existingTags.Add(temp);
        }

        if (_forumInteractive.Forums is null)
        {
            return null;
        }
        var forums = _forumInteractive.Forums
            .Where(f => existingTags
            .Count(et => et.Forums is not null &&
            et.Forums.Contains(f)) == existingTags.Count).ToList();

        return [.. forums.Distinct().OrderByDescending(i => i.Created_at)];
    }

    private void InitialIndexForums(List<Forum>? forums)
    {
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
            }
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

    public IActionResult Login(string? username)
    {
        if (username is not null)
        {
            _userInteractive.User = new User();
            var user = _userInteractive.GetUserByUserName(username);
            if (user is not null)
            {
                _userInteractive.User = user;
            }
            else
            {
                user = _userInteractive.CreateUser(username);
                _userInteractive.User = user;
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
        if (_userInteractive.User is null)
        {
            var viewmodels = new HomeViewModels(null, null, null, null);
            return View(viewmodels);
        }
        else
        {
            var viewmodels = new HomeViewModels(null, _userInteractive.GetAllUser(), _userInteractive.User, null);
            return View(viewmodels);
        }
    }

    [HttpPost]
    public IActionResult CreateForum(Forum forum)
    {
        if (_userInteractive.User is null)
            return View();
        else
        {
            forum.User = _userInteractive.User;
            _forumInteractive.CreateForum(forum);
        }
        return RedirectToAction("Index");
    }



    public IActionResult EditForum(int? Id)
    {
        if (Id is null || Id == 0 || _forumInteractive.Forums is null)
            return NotFound();

        var forum = _forumInteractive.Forums.FirstOrDefault(s => s.ForumId == Id);
        if (forum is null)
            return NotFound();

        var viewmodels = new HomeViewModels(null, null, _userInteractive.User, forum);

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

            if (_forumInteractive.Forums is not null && _userInteractive.User is not null)
            {
                if (_forumInteractive.Forums.Any(f => f.ForumId == forum.ForumId) &&
                (forum.UserId == _userInteractive.User.Id ||
                _userInteractive.User.Role == "admin"))
                {
                    _forumInteractive.EditForum(forum);
                }
            }
            return RedirectToAction("Index");
        }
        return View(forum);
    }

    public IActionResult DeleteForum(int? id)
    {
        var forum = _forumInteractive.Forums?.FirstOrDefault(f => f.ForumId == id);
        if (id is not null &&
            forum is not null &&
            _userInteractive.User is not null &&
            (_userInteractive.User.Id == forum.UserId || _userInteractive.User.Role == "admin"))
        {
            _forumInteractive.RemoveForum(id);
            return RedirectToAction("Index");
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult PostComment(Comment comment)
    {
        if (ModelState.IsValid)
        {
            if (comment.Body != string.Empty && _userInteractive.User is not null)
            {
                comment.UserId = _userInteractive.User.Id;

                if (_forumInteractive.Forums is not null)
                {
                    var forum = _forumInteractive.Forums.First(ff => ff.ForumId == comment.ForumId);
                    comment.Forum = forum;

                    _commentInteractive.Create(comment);
                }
                return NoContent();
            }
        }
        return RedirectToAction("Index");
    }

    public IActionResult EditComment(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        if (_userInteractive.User is not null)
        {
            if (_userInteractive.User.Role == "admin")
            {
                var comment = _commentInteractive.GetById((int)id);

                if (comment is not null)
                    return View(comment);
            }
            else if (_userInteractive.User.Comments is not null)
            {
                var comment = _userInteractive.User.Comments
                    .FirstOrDefault(c => c.CommentId == id);

                if (comment is not null)
                    return View(comment);
            }
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult EditComment(Comment? comment)
    {
        if (comment is null)
        {
            return NotFound();
        }

        _commentInteractive.Edit(comment);
        return RedirectToAction("Index");
    }

    public IActionResult DeleteComment(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        if (_userInteractive.User is not null)
        {
            if (_userInteractive.User.Role == "admin")
                _commentInteractive.Delete((int)id);
            else if (_userInteractive.User.Comments is not null)
            {
                var comment = _userInteractive.User.Comments
                    .FirstOrDefault(c => c.CommentId == id);
                if (comment is not null)
                {
                    _commentInteractive.Delete((int)id);
                }
            }
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult FilterTags(List<string> tagFilters)
    {
        TempData["tagFilters"] = JsonSerializer.Serialize(tagFilters);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}