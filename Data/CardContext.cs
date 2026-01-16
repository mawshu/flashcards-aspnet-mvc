using Microsoft.EntityFrameworkCore;
using SergeevaMaria_Lab6_V2_1.Models;
using System.Collections.Generic;

namespace SergeevaMaria_Lab6_V2_1.Data
{
    public class CardContext : DbContext
    {
        public CardContext(DbContextOptions<CardContext> options) : base(options) { }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
