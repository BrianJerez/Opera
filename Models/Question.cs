using System.ComponentModel.DataAnnotations;
using System;

namespace Opera.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        public string QuestionTitle { get; set; }

        public string QuestionDescription { get; set; }
        
        public int QuestionVotes { get; set; }

        public string UserId { get; set; }

        public DateTime QuestionDate { get; set; }
    }
}