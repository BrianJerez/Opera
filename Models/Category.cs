using System.ComponentModel.DataAnnotations;

namespace Opera.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        public string Title_Category { get; set; }
        
        public string Description_Category { get; set; }
    }
}