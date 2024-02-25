using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity

    {
        [Table("T_Category")]
        public class Category
        {
            [Key]
            public int CategoryId { get; set; }

            [Required]
            [MaxLength(50)]  
            public string Title { get; set; } = string.Empty;

            public string Description { get; set; } = string.Empty; 

            //hnaya drt relation dyal one to many whch will create a conjunction table
            public List<Post> Posts { get; set; } = new List<Post>();
        public Category() { }
        public Category(int categoryId, string title, string description, List<Post> posts)
        {
            CategoryId = categoryId;
            Title = title;
            Description = description;
            Posts = posts;
        }
    }

    }
