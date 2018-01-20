using System.ComponentModel.DataAnnotations;

namespace Opera.Models
{
    public class Answer
    {
        [Key]
        public int Id_Answer { get; set; }
        
        public string Content_Answer { get; set; }
    }
}