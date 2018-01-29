using System;
using Opera.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opera.Models
{
    public class QuestionReport
    {
        [Key]
        public int ReportId { get; set; }
        
        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
    }
}