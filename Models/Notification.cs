using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opera.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        public string UserName { get; set; }

        public DateTime Date { get; set; }

        public bool Seen { get; set; }

        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
    }
}