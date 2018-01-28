using System;
using System.Linq;
using Opera.Models;

namespace Opera.ViewModels
{
    public class PaginationViewModel
    {
        public int AmmountOfPages { get; set; }
        public IQueryable<Question> Questions { get; set; }
    }
}