using System.Collections.Generic;
using System;

namespace XKANBAN.DTOs.Projects
{
    public class TemplateViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDelete { get; set; }
        public List<CartTemplateViewModel> CartTemplates { get; set; }
        public List<ColumnTemplateViewModel> ColumnTemplates { get; set; }
    }

    public class CartTemplateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public DateTime DeadLineDate { get; set; }
        public bool IsDelete { get; set; }
    }

    public class ColumnTemplateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SlotNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }

}
