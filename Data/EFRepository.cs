using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SergeevaMaria_Lab6_V2_1.Models;

namespace SergeevaMaria_Lab6_V2_1.Data
{
    public class EFRepository : ICardRepository
    {
        private readonly CardContext context;

        public EFRepository(CardContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Card> GetAllCards()
        {
            return context.Cards.Include(c => c.Category).ToList();
        }

        public bool AddCard(Card card)
        {
            if (context.Cards.Any(c => c.Word == card.Word && c.Translation == card.Translation))
                return false;

            context.Cards.Add(card);
            context.SaveChanges();
            return true;
        }

        public bool RemoveCard(Card card)
        {
            context.Cards.Remove(card);
            context.SaveChanges();
            return true;
        }

        public bool UpdateCard(Card card)
        {
            context.Cards.Update(card);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<Category> GetCategories()
        {
            return context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public bool AddCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
            return true;
        }

        public bool UpdateCategory(Category category)
        {
            context.Categories.Update(category);
            context.SaveChanges();
            return true;
        }

        public bool RemoveCategory(Category category)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
            return true;
        }

    }
}
