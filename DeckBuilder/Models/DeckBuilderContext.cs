using Microsoft.EntityFrameworkCore;

namespace DeckBuilder.Models
{
    public class DeckBuilderContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }           // Cards
		public DbSet<CardColor> CardColors { get; set; }	// Card colors
        public DbSet<Set> Sets { get; set; }             // Card set
        public DbSet<Booster> Boosters { get; set; }     // Booster for set
        public DbSet<Deck> Decks { get; set; }           // Deck
        public DbSet<Decklist> DeckLists { get; set; }   // Deck list
		public DbSet<DeckComment> DeckComments { get; set; }

		public DeckBuilderContext(DbContextOptions<DeckBuilderContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			// Composite primary keys in database
            modelBuilder.Entity<Decklist>().HasKey(d => new { d.DeckId, d.CardId });
			modelBuilder.Entity<Booster>().HasKey(b => new { b.SetCode, b.CardType });
			modelBuilder.Entity<CardColor>().HasKey(c => new { c.CardId, c.Color });
			modelBuilder.Entity<DeckComment>().HasKey(c => new { c.DeckId, c.CommentId });

			// Relationships
        }
        
    }
}