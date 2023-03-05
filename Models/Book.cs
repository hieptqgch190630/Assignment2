using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class Book
    {
        public int Id { get; set; }
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        [Range(1,100)]
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }

    }
}
