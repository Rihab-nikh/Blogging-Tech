using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace  Models
{
    public class UserVM 
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

        public List<PostVM>? Posts { get; set; }

        public UserVM(int UserId,String Username,String Email,String Password,String ProfileImage)
        {
            this.UserId= UserId;
            this.Username = Username;
            this.Email = Email;
            this.Password = Password;
            this.ProfileImage = ProfileImage;


        }
        public UserVM()
        {

        }
    }
}