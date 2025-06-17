using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XKANBAN.Models.Project
{
    public class ChatMessage
    {
        [Key]
        public int ChatMessageId { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime TimeSent { get; set; }

        [Required]
        public string UserName { get; set; }

        public string FilePath { get; set; }

        public string ReadByUsernames { get; set; }

        [ForeignKey("Card")]
        public int CardId { get; set; }

        public Cart Card { get; set; }

    }
}