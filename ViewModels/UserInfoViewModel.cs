using System;
using System.Linq;
using Opera.Models;
using System.Collections.Generic;


namespace Opera.ViewModels
{
    class UserInfoViewModel
    {
        public CustomUserFields UserInfo { get; set; }
        public IQueryable<Question> Questions { get; set; }
        public IQueryable<Answer> Answers { get; set; }
    }
}