using System;
using Opera.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opera.Models
{
    public class AnswerReport
    {
        [Key]
        public int ReportId { get; set; }
        
        public int AnswerId { get; set; }

        [ForeignKey("AnswerId")]
        public virtual Answer Answer { get; set; }
    }
}