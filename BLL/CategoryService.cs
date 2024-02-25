using DAL.Repos;
using DAL.Entity;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class CategoryService
    {
        private readonly CategoryRepo _categoryRepo;

        public CategoryService(CategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        private List<PostVM> MapPostsToVMs(List<Post> posts)
        {
            return posts.Select(post => new PostVM
            {
                PostId = post.PostId,
                Title = post.Title,
                Content = post.Content,
                CategoryId = post.CategoryId
            }).ToList();
        }

        private List<Post> MapPostsToEntities(List<PostVM> postVMs)
        {
            return postVMs.Select(postVM => new Post
            {
                PostId = postVM.PostId,
                Title = postVM.Title,
                Content = postVM.Content,
                CategoryId = postVM.CategoryId
            }).ToList();
        }

        public CategoryVM CreateCategory(CategoryVM categoryVM)
        {
            var category = new Category
            {
                CategoryId = categoryVM.CategoryId,
                Title = categoryVM.Title,
                Description = categoryVM.Description,
                Posts = MapPostsToEntities(categoryVM.Posts)
            };

            var createdCategory = _categoryRepo.Create(category);

            return new CategoryVM
            {
                CategoryId = createdCategory.CategoryId,
                Title = createdCategory.Title,
                Description = createdCategory.Description,
                Posts = MapPostsToVMs(createdCategory.Posts)
            };
        }

        public CategoryVM GetCategoryById(int categoryId)
        {
            var category = _categoryRepo.GetById(categoryId);


            return new CategoryVM
            {
                CategoryId = category.CategoryId,
                Title = category.Title,
                Description = category.Description,
                Posts = MapPostsToVMs(category.Posts),
               
            };
        }

        public IEnumerable<CategoryVM> GetAllCategories()
        {
            return _categoryRepo.GetAllCategories()
                .Select(category => new CategoryVM
                {
                    CategoryId = category.CategoryId,
                    Title = category.Title,
                    Description = category.Description,
                    Posts = MapPostsToVMs(category.Posts)
                })
                .ToList();
        }

        public void UpdateCategory(CategoryVM categoryVM)
        {
            var category = _categoryRepo.GetById(categoryVM.CategoryId);

            if (category != null)
            {
                category.Title = categoryVM.Title;
                category.Description = categoryVM.Description;
                category.Posts = MapPostsToEntities(categoryVM.Posts);

                _categoryRepo.Update(category);
            }
        }

        public void DeleteCategory(int categoryId)
        {
            _categoryRepo.Delete(categoryId);
        }
    }
}
