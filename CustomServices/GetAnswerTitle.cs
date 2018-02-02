using System;
using System.Linq;
using Opera.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Opera.CustomServices
{
    public class GetAnswerTitle
    {
        private OperaDataContext _db { get; set; }
        public GetAnswerTitle(OperaDataContext db)
        {
            _db = db;
        }

        public String AnswerTitle(int Id)
        {
            return _db.Questions.FirstOrDefaultAsync(x => x.QuestionId == Id).Result.QuestionTitle;
        }
    }
}