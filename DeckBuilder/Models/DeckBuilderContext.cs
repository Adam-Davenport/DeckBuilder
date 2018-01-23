using Microsoft.EntityFrameworkCore;

namespace DeckBuilder.Models
{
    public class DeckBuilderContext : DbContext
    {
        public DbSet<Card> Card { get; set; }           // Cards
        public DbSet<Set> Set { get; set; }             // Card set
        public DbSet<Booster> Booster { get; set; }     // Booster for set
        public DbSet<Deck> Deck { get; set; }           // Deck
        public DbSet<Decklist> DeckList { get; set; }   // Deck list

        public DeckBuilderContext(DbContextOptions<DeckBuilderContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Decklist>().HasKey(d => new { d.DeckId, d.CardId });
        }
        
    }
}