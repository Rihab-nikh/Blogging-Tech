using BLL;
using Microsoft.AspNetCore.Mvc;


namespace Blogging_Tech.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategories();

            return View(categories);


        }
        public IActionResult Art()
        {
            var categories = _categoryService.GetCategoryById(1);

            return View(categories);

        }


        public IActionResult VideoEditing()
        {
            var categories = _categoryService.GetCategoryById(4);

            return View(categories);

        }
        public IActionResult FilmMaking()
        {
            var categories = _categoryService.GetCategoryById(3);
            return View(categories);

        }


        public IActionResult Adobe()
        {
            var categories = _categoryService.GetCategoryById(2);

            return View(categories);

        }
        public IActionResult Programming()
        {
            var categories = _categoryService.GetCategoryById(8);

            return View(categories);
        }
        public IActionResult ArtificialIntelligence()
        {
            var categories = _categoryService.GetCategoryById(6);

            return View(categories);



        }
        public IActionResult Blockchain()
        {
            var categories = _categoryService.GetCategoryById(5);

            return View(categories);


        }
        public IActionResult MicroControllers()
        {
            var categories = _categoryService.GetCategoryById(7);

            return View(categories);

        }

    }
}