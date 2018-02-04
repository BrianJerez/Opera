using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Opera.Models
{
    public class OperaDataContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<QuestionReport> QuestionReports { get; set; }
        public DbSet<AnswerReport> AnswerReports { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        
        public OperaDataContext(DbContextOptions<OperaDataContext> options)
        :base(options)
        {
            Database.EnsureCreated();
        }
    }
}