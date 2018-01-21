using Microsoft.EntityFrameworkCore;

namespace DeckBuilder.Models
{
    class DeckBuilderContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<Deck> Decks { get; set; }

        public DeckBuilderContext(DbContextOptions<DeckBuilderContext> options) : base(options)
        {
        }
        
    }
}