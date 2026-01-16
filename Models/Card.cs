using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SergeevaMaria_Lab6_V2_1.Models
{
    public class Card
    {
        public int Id { get; set; }

        [Required]
        public string Word { get; set; }

        [Required]
        public string Translation { get; set; }
        public List<string> Examples { get; set; } = new List<string>();

        [Required]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
