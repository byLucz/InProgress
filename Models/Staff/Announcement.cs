using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace XKANBAN.Models.Project
{
    public class SAnnouncement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; } 

        #region Relations
            public User.User User { get; set; }

        #endregion
    }
}