using System.Collections.Generic;
using SergeevaMaria_Lab6_V2_1.Models;

namespace SergeevaMaria_Lab6_V2_1.Data
{
    public interface ICardRepository
    {
        IEnumerable<Card> GetAllCards();
        bool AddCard(Card card);
        bool RemoveCard(Card card);
        bool UpdateCard(Card card);

        IEnumerable<Category> GetCategories();
        Category GetCategory(int id);
        bool AddCategory(Category category);
        bool UpdateCategory(Category category);
        bool RemoveCategory(Category category);

    }
}
