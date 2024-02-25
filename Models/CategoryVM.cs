using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Models
{
    public class CategoryVM
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        //hnaya drt relation dyal one to many whch will create a conjunction table
        public List<PostVM> Posts { get; set; } = new List<PostVM>();
        
        public CategoryVM() { }
        public CategoryVM(int categoryId, string title, string description, List<PostVM> posts)
        {
            CategoryId = categoryId;
            Title = title;
            Description = description;
            Posts = posts;
            
        }
    }

}
