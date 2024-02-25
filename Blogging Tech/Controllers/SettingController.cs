using DAL.Entity;
using DAL.Repos;
using Microsoft.AspNetCore.Mvc;

namespace Blogging_Tech.Controllers
{
    public class SettingController : Controller
    {
        private readonly UserRepo _userRepo;
        private readonly PostRepo _postRepo;
        private readonly CategoryRepo _categoryRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserService _userService;

        public SettingController(UserRepo userRepo, UserService userService, PostRepo postRepo,
            CategoryRepo categoryRepo,
            IWebHostEnvironment webHostEnvironment)
        {
            _userRepo = userRepo;
            _postRepo = postRepo;
            _categoryRepo = categoryRepo;
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChangePassword(string currentPassword, string newPassword)
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int userId))
            {
                var user = _userService.GetUserById(userId);

                if (user.Password == currentPassword)
                {
                    _userService.UpdatePassword(userId, newPassword);
                }
                else
                {
                    ModelState.AddModelError("CurrentPassword", "Incorrect current password.");
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult ChangeProfilePicture(IFormFile profileImage)
        {
            if (profileImage != null && profileImage.Length > 0)
            {
                var userIdString = HttpContext.Session.GetString("UserId");

                if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int userId))
                {
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads\\img\\profile");

                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }

                    var fileName = $"{userId}_profile.jpg";
                    var filePath = Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        profileImage.CopyTo(stream);
                    }

                    var user = _userRepo.GetById(userId);
                    user.ProfileImage = $"/uploads/img/profile/{fileName}";
                    _userRepo.Update(user);

                    return RedirectToAction("Index");
                }
            }

            TempData["Error"] = "Error uploading profile image.";
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public IActionResult ChangeEmail(User newUser)
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int userId))
            {
                var user = _userService.GetUserById(userId);
                _userService.ChangeEmail(user.UserId,newUser.Email);

                var updatedUser = _userService.GetUserById(userId);
                return View("Index", updatedUser);
            }

            return RedirectToAction("Index");
        }
    }
}
