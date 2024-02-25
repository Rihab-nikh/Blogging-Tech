using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Microsoft.EntityFrameworkCore;
namespace DAL.Repos
{
    public class CategoryRepo
    {
        private MyDbContext _dbContext;

        public CategoryRepo(MyDbContext x)
        {
            _dbContext = x;
        }

        public Category Create(Category category)
        {
            _dbContext.Category.Add(category);
            _dbContext.SaveChanges();
            return category;
        }

        public void Delete(int categoryId)
        {
            var category = _dbContext.Category.Find(categoryId);
            if (category != null)
            {
                _dbContext.Category.Remove(category);
                _dbContext.SaveChanges(); 
            }
        }
        public void Update(Category category)
        {
            _dbContext.Category.Update(category);
            _dbContext.SaveChanges(); 
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _dbContext.Category.ToList(); 
        }
        public Category? GetById(int categoryId)
        {
            return _dbContext.Category
                .Include(c => c.Posts) 
                .FirstOrDefault(c => c.CategoryId == categoryId);
        }





    }
}
