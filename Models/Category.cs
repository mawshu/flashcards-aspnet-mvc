using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SergeevaMaria_Lab6_V2_1.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Название' обязательно")]

        public string Title { get; set; }

        public string Description { get; set; }

        public List<Card> Cards { get; set; } = new List<Card>();
    }
}
