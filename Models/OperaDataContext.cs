using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Opera.Models
{
    public class OperaDataContext : DbContext
    {
        List<Question> Questions = new List<Question>();
        List<Answer> Answers = new List<Answer>();
        List<Category> Categories = new List<Category>();
        public OperaDataContext(DbContextOptions<OperaDataContext> options)
        :base(options)
        {
            Database.EnsureCreated();
        }
    }
}