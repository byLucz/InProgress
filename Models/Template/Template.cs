using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DIPLOMKANBAN.Models.Template
{
    public class Template
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDelete { get; set; }
        public ICollection<CartTemplate> CartTemplates { get; set; } = new List<CartTemplate>();
        public ICollection<ColumnTemplate> ColumnTemplates { get; set; } = new List<ColumnTemplate>();
    }

    public class CartTemplate
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public DateTime DeadLineDate { get; set; }
        public bool IsDelete { get; set; }
        public int TemplateId { get; set; }
        public Template Template { get; set; }
    }

    public class ColumnTemplate
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int SlotNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int TemplateId { get; set; }
        public Template Template { get; set; }
    }
}