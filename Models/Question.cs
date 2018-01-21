using System.ComponentModel.DataAnnotations;
using System;

namespace Opera.Models
{
    public class Question
    {
        [Key]
        public int Id_Question { get; set; }

        public string Title_Question { get; set; }

        public string Description_Question { get; set; }
        
        public int Votes_Question { get; set; }

        public string Id_User { get; set; }

        public DateTime Date_Question { get; set; }
    }
}