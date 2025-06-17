using System;
using System.ComponentModel.DataAnnotations;

namespace XKANBAN.DTOs.Projects
{
    public class AddProjectViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class AddCartViewModel
    {
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}