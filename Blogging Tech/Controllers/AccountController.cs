
using Microsoft.AspNetCore.Mvc;
using DAL.Repos;
using Models;

namespace Blogging_Tech.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepo _userRepo;
        private readonly PostRepo _postRepo;
        private readonly CategoryRepo _categoryRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserService _userService;

        public AccountController(
            UserRepo userRepo,
            PostRepo postRepo,
            CategoryRepo categoryRepo,
            IWebHostEnvironment webHostEnvironment,
            UserService userService)
        {
            _userRepo = userRepo;
            _postRepo = postRepo;
            _categoryRepo = categoryRepo;
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
        }

        public IActionResult Profile()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (userIdString == null)
            {
                return RedirectToAction("Index", "Auth");
            }

            var userId = int.Parse(userIdString);

            var user = _userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound();
            }

            var posts = _postRepo.GetAllPosts().OrderByDescending(p => p.PublicationDate)
                .Select(post => new PostVM
                {
                    PostId = post.PostId,
                    Title = post.Title,
                    CategoryId = post.CategoryId,
                    CategoryName = post.CategoryName,
                    Content = post.Content,
                    Image = post.Image,
                    PublicationDate = post.PublicationDate,
                    UserId = userId,
                }).ToList();

            var categories = _categoryRepo.GetAllCategories()
                .Select(category => new CategoryVM
                {
                    CategoryId = category.CategoryId,
                    Title = category.Title,
                    Description = category.Description,
                    Posts = posts,
                }).ToList();

            var blogVM = new BlogVM(user,posts,categories);

            return View(blogVM);
        }

        
    }
}
