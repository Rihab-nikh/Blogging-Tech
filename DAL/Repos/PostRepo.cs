using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class PostRepo
    {
        private MyDbContext _dbContext;

        public PostRepo(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Post Create(Post post)
        {
            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();
            return post;
        }

        public void Delete(int postId)
        {
            var post = _dbContext.Posts.Find(postId);
            if (post != null)
            {
                _dbContext.Posts.Remove(post);
                _dbContext.SaveChanges();
            }
        }
        
        public IEnumerable<Post> GetAllPosts()
        {
            return _dbContext.Posts.ToList();
        }

        public void Update(Post post)
        {
            _dbContext.Posts.Update(post);
            _dbContext.SaveChanges();
        }

    }
}
