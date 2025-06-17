using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace XKANBAN.Models.Project
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [MaxLength(200)]
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public int ProjectId { get; set; }

        public int StatusNumber { get; set; }

        public DateTime DeadLineDate { get; set; }

        public string Color { get; set; }

        public bool IsDelete { get; set; }
        public int? RequestedColumnId { get; set; }

        // Внешний ключ для связи с колонками
        [ForeignKey("SColumn")]
        public int? SColumnId { get; set; }
        public Column SColumn { get; set; }

        #region Relations

        public Project Project { get; set; }

        public ICollection<ChatMessage> ChatMessages { get; set; }

        #endregion
    }
}