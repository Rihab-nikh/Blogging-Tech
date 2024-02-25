using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    public class UserRepo
    {
        private readonly MyDbContext _dbContext;

        public UserRepo(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User Create(User user)
        {
            _dbContext.User.Add(user);
            _dbContext.SaveChanges();  
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.User.ToList();
        }

        public User? Update(User updatedUser)
        {
            var existingUser = _dbContext.User.Find(updatedUser.UserId);//hadi requete juste pour le primarykey

            if (existingUser != null)
            {

                existingUser.Username = updatedUser.Username;
                existingUser.Email = updatedUser.Email;
                existingUser.Password = updatedUser.Password;
                existingUser.ProfileImage = updatedUser.ProfileImage;

                _dbContext.SaveChanges();
            }

            return existingUser;
        }

        public void Delete(int userId)
        {
            var user = _dbContext.User.Find(userId);
            if (user != null)
            {
                _dbContext.User.Remove(user);
                _dbContext.SaveChanges();
            }
        }

        

        public User? GetByUsername(string username)
        {
            return _dbContext.User.FirstOrDefault(u => u.Username == username);
        }

        public User? GetById(int userId)
        {
            return _dbContext.User.Include(u => u.Posts).FirstOrDefault(u => u.UserId == userId);
        }
    }
}
