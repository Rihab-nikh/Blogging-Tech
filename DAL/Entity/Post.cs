using DAL.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    [Table("T_Post")]
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; } = DateTime.UtcNow;
        public string Image { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        [NotMapped]
        public string CategoryName { get; set; } = string.Empty;

        public Post(int postId, string title, string content, DateTime publicationDate, string image, int userId, int categoryId, string categoryName)
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
         public Post()
        {

        }


    }
}