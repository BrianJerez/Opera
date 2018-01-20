using System.ComponentModel.DataAnnotations;

namespace Opera.Models
{
    public class Question
    {
        [Key]
        public int Id_Question { get; set; }

        public string Title_Question { get; set; }

        public string Description_Question { get; set; }
        
        public int Votes_Question { get; set; }
    }
}