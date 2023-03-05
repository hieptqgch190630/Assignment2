using System.ComponentModel.DataAnnotations;
using System;

namespace Assignment.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string Title { get; set; }

        public Book Book { get; set; }
        public string UserEmail { get; set; }

        [Required]
        public int OrderQuantity { get; set; }

        [Required]
        public double OrderPrice { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }
    }
}
