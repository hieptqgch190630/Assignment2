using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<Book> Book { get; set; }
    }
}
