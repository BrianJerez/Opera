using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opera.Models
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        
        public string Content_Answer { get; set; }


        public int QuestionId { get; set; }
        
        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
    }
}