using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XKANBAN.Models.Project
{
    public class Column
    {
        [Key]
        public int ColumnId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public int SlotNumber { get; set; }

        public bool IsActive { get; set; } = true;

        #region Relations

        public Project Project { get; set; }
        public List<Cart> Carts { get; set; }

        #endregion
    }
}