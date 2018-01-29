using System;
using System.Linq;
using System.Collections.Generic;
using Opera.Models;

namespace Opera.ViewModels
{
    public class QuestionViewModel
    {
        public Question GetQuestion { get; set; }
        public IQueryable<Answer> Answers { get; set; }

        public string AnswerContent { get; set; }

        public CustomUserFields UserFieldsInfo { get; set; }
    }
}