using Microsoft.EntityFrameworkCore;

namespace DeckBuilder.Models
{
    public class DeckBuilderContext : DbContext
    {
        public DbSet<Card> Card { get; set; }
        public DbSet<Set> Set { get; set; }
        public DbSet<Deck> Deck { get; set; }

        public DeckBuilderContext(DbContextOptions<DeckBuilderContext> options) : base(options)
        {
        }
        
    }
}