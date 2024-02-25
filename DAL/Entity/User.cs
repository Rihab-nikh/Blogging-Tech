using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    [Table("T_User")]

    public class User
    {
        [Key]
        public int UserId { get; set; }
       
        public string Role { get; set; } = "ordinary";
        public String Username { get; set; } = String.Empty;
        [Required]
        public String Password { get; set; } = String.Empty;
        [Required]
        public String Email { get; set; } = String.Empty;
        
        public String ProfileImage { get; set; } = String.Empty;

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public List<Post>? Posts { get; set; }

        public User(String Username, String Email, String Password, String ProfileImage)
        {
            this.Username = Username;
            this.Email = Email;
            this.Password = Password;
            this.ProfileImage = ProfileImage;


        }
        public User()
        {

        }

    }
}
