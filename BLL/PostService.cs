using DAL.Repos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Models;
using DAL.Entity;
using System.Reflection.Metadata;

namespace BLL
{
    public class PostService
    {
        private readonly PostRepo _postRepo;
        private readonly MyDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostService(PostRepo postRepo, MyDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _postRepo = postRepo;
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        private List<PostVM> MapPostsToVMs(List<Post> posts)
        {
            return posts.Select(post => new PostVM
            {
                PostId = post.PostId,
                Title = post.Title,
                Content = post.Content,
                PublicationDate = post.PublicationDate,
                UserId = post.UserId,
            CategoryId = post.CategoryId,
            CategoryName = post.CategoryName,
            Image = post.Image
            }).ToList();
        }

        public PostVM CreatePost(PostVM postVM)
        {
            var post = new Post
            {
                Title = postVM.Title,
                Content = postVM.Content,
                PublicationDate = DateTime.UtcNow,
                UserId = GetCurrentUserId(),
                CategoryId = postVM.CategoryId,
                CategoryName = postVM.CategoryName,
                Image = postVM.Image,
                PostId = postVM.PostId
            };

            var createdPost = _postRepo.Create(post);

            return new PostVM
            {
                PostId = createdPost.PostId,
                Title = createdPost.Title,
                Content = createdPost.Content,
                PublicationDate = createdPost.PublicationDate,
                UserId = createdPost.UserId,
                CategoryId = createdPost.CategoryId,
                CategoryName = createdPost.CategoryName,
                Image = createdPost.Image,
            };
        }

        private int GetCurrentUserId()
        {
            var userIdString = _httpContextAccessor.HttpContext?.Session.GetString("UserId");

            if (userIdString != null && int.TryParse(userIdString, out int userId))
            {
                return userId;
            }

            return 1;
        }

        public IEnumerable<PostVM> GetPostsByUserId(int userId)
        {
            return _dbContext.Posts.Where(p => p.UserId == userId)
                                   .Select(p => new PostVM
                                   {
                                       PostId = p.PostId,
                                       Title = p.Title,
                                       Content = p.Content,
                                       PublicationDate = p.PublicationDate,
                                       UserId = p.UserId,
                                       CategoryId = p.CategoryId,
                                       CategoryName = p.CategoryName,
                                       Image = p.Image
                                       
                                   })
                                   .ToList();
        }

        public IEnumerable<PostVM> GetPosts()
        {
            return _postRepo.GetAllPosts().Select(p => new PostVM
            {
                PostId = p.PostId,
                Title = p.Title,
                Content = p.Content,
                PublicationDate = p.PublicationDate,
                UserId = p.UserId,
                CategoryId= p.CategoryId,
                CategoryName = p.CategoryName,
                Image= p.Image
            }).ToList();
        }

        public IEnumerable<PostVM> GetFeaturedPosts()
        {
            var featuredPostIds = Enumerable.Range(1, 10).OrderBy(x => Guid.NewGuid()).Take(3).ToList();
            return _dbContext.Posts.Where(p => featuredPostIds.Contains(p.PostId))
                                   .Select(p => new PostVM
                                   {
                                       PostId = p.PostId,
                                       Title = p.Title,
                                       Content = p.Content,
                                       PublicationDate = p.PublicationDate,
                                       UserId = p.UserId,
                                       CategoryId= p.CategoryId,
                                       CategoryName = p.CategoryName,
                                       Image= p.Image
                                   })
                                   .ToList();
        }

        public void UpdatePost(PostVM postVM)
        {
            var post = _dbContext.Posts.Find(postVM.PostId);

            if (post != null)
            {
                post.Title = postVM.Title;
                post.Content = postVM.Content;
                post.PublicationDate = DateTime.UtcNow;
                post.UserId = GetCurrentUserId();

                _postRepo.Update(post);
            }
            
        }

        public void DeletePost(int postId)
        {
            _postRepo.Delete(postId);
        }

        public List<Post> GetPostsByCategory(string categoryName)
        {
            return _dbContext.Posts.Where(p => p.CategoryName == categoryName).ToList();
        }
    }
}
