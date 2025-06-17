using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace XKANBAN.DTOs.Projects
{
    public class AddAnnouncementViewModel
    {
        [Required]
        public int AnnouncementId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDelete { get; set; }
    }
}