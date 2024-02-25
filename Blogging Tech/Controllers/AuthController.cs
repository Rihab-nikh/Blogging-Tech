using DAL.Repos;
using Microsoft.AspNetCore.Mvc;
using Models;
using DAL;
using DAL.Entity;

namespace Blogging_Tech.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserRepo _userRepo;
        private readonly MyDbContext _dbContext; 


        public AuthController(MyDbContext dbContext)
        {
            _userRepo = new UserRepo(dbContext); 
            _dbContext = dbContext; 

        }

        public IActionResult Index(UserVM userModel, bool rememberMe)
        {

            if (HttpContext.Session.GetString("UserId") != null)
            {

                return RedirectToAction("Index", "Home");
            }

            var user = _userRepo.GetByUsername(userModel.Username);

            if (user != null && userModel.Password == user.Password)
            {

                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("Username", user.Username);

                if (rememberMe)
                {
                    Response.Cookies.Append("UserId", user.UserId.ToString(), new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(7)
                    });
                }



                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("InvalidCredentials", "Invalid username or password");
                return View("Index", userModel);
            }
        }

        public IActionResult SignUp(UserVM userModel)
        {
            if (ModelState.IsValid)
            {
                // bdit kamappi UserVM to User
                var newUser = new User(userModel.Username,userModel.Email,userModel.Password,userModel.Email);
                var userRepo = new UserRepo(_dbContext);
                userRepo.Create(newUser); 
                return RedirectToAction("Index", "Auth");
            }

            return View("SignUp", userModel);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("UserId");

            return RedirectToAction("Index", "Home");
        }
    }

}

