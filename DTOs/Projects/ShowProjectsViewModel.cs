using Microsoft.VisualBasic;
using System;

namespace XKANBAN.DTOs.Projects
{
    public class ShowProjectsViewModel
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Descrioption { get; set; }
        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AssingsIds { get; set; }
    }
}