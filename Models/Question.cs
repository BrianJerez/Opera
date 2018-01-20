using System.ComponentModel.DataAnnotations;

namespace Opera.Models
{
    class Question
    {
        public long Id_Question { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Votes { get; set; }
    }
}