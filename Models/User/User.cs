using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XKANBAN.Models.User
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "FullName")]
        [MaxLength(250)]
        [Required]
        public string FullName { get; set; }

        [Display(Name = "Username")]
        [MaxLength(250)]
        [Required]
        public string Username { get; set; }

        [Display(Name = "Email")]
        [MaxLength(250)]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [MaxLength(250)]
        [Required]

        public string Password { get; set; }

        public string Group { get; set; }
        
        public DateTime RegisterDate { get; set; }
        public bool IsDelete { get; set; }
        public string Role { get; set; }

        #region Relations

        public List<Project.Project> Projects { get; set; }
            
        #endregion
    }
}