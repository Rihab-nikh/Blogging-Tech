using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace  Models
{
    public class PostVM
    {    
        [Key]
        public int PostId { get; set; }
        [Required]
        [MaxLength(100)]
        public String Title { get; set; } = String.Empty;
        public String Content { get; set; } = String.Empty;
        public DateTime PublicationDate { get; set; } = DateTime.UtcNow;
        public String Image { get; set; } = String.Empty;
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public PostVM() { }
        public PostVM(int postId, string title, string content, DateTime publicationDate, string image, int userId, int categoryId, string categoryName)
        {
            PostId = postId;
            Title = title;
            Content = content;
            PublicationDate = publicationDate;
            Image = image;
            UserId = userId;
            CategoryId = categoryId;
            CategoryName = categoryName;
        }
    }
}
