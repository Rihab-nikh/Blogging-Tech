using Microsoft.Extensions.Hosting;
using Models;

namespace Models
{
    public class BlogVM
    {
        public UserVM? User { get; set; }
        public List<PostVM>? Posts { get; set; }
        public List<CategoryVM>? Categories { get; set; }

        public BlogVM() { }
        public BlogVM(UserVM? user, List<PostVM>? posts, List<CategoryVM>? categories)
        {
            User = user;
            Posts = posts;
            Categories = categories;
        }
    }
}