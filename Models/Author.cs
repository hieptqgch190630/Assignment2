using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class Author
    {
        public int Id { get; set; }
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Country { get; set; }
        public ICollection<Book> Book { get; set; }
    }
}
