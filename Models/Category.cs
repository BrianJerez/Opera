using System.ComponentModel.DataAnnotations;

namespace Opera.Models
{
    public class Category
    {
        public int Id_Category { get; set; }
        public string Title_Category { get; set; }
        public string Description_Category { get; set; }
    }
}