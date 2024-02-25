using DAL.Repos;
using DAL.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Models;
using BLL;
using DAL;

namespace Blogging_Tech.Controllers
{
    public class PostController : Controller
    {
        private readonly PostRepo _postRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly CategoryRepo _categoryRepo;
        private readonly MyDbContext _dbContext;
        private readonly PostService _postService;
        public PostController(MyDbContext dbContext,PostRepo postRepo, CategoryRepo categoryRepo, IWebHostEnvironment webHostEnvironment, PostService postService)
        {
            _postRepo = postRepo;
            _webHostEnvironment = webHostEnvironment;
  
            _categoryRepo = categoryRepo; 

            _dbContext = dbContext;
            _postService = postService;


        }
       
        public IActionResult Adding(PostVM pst, IFormFile image)
        {
            string? Id = HttpContext.Session.GetString("UserId");

            var selectedCategory = _categoryRepo.GetById(pst.CategoryId);

            Post p = new Post(pst.PostId, pst.Title, pst.Content, pst.PublicationDate, pst.Image, pst.UserId, pst.CategoryId, pst.CategoryName) ;

            if (image != null && image.Length > 0)
            {
                var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads\\img\\posts");

                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                if (image.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName.Value.Trim('"');

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                        p.Image = $"/uploads/img/posts/{fileName}";
                    } 
                }
            }
            

            _postRepo.Create(p);
            



            return RedirectToAction("Profile", "Account");
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            _postService.DeletePost(id);
            return RedirectToAction("Profile", "Account");
        }
        public IActionResult Articles()
        {
            var posts = _postService.GetPosts().OrderByDescending(p => p.PublicationDate); 
            return View(posts);
        }



    }
}
