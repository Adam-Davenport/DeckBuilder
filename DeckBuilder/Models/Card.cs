using DeckBuilder.DTO;

namespace DeckBuilder.Models
{
    public class Card
    {
        public string Id { get; set; } // Unique id of the card for a specific set
        public string MultiverseId { get; set; } // Multiverse id of the card
        public string Name { get; set; }
        public string SetId { get; set; }
        public int Number { get; set; }
        public string ManaCost { get; set; }
        public int Cmc { get; set; }
        public string Power { get; set; }
        public string Toughness { get; set; }
        public string Type { get; set; }
        public string ImageUrl { get; set; }
        public string Rarity { get; set; }
        public string Text { get; set; }
        public string FlavorText { get; set; }
        public string Artist { get; set; }
        public string Layout { get; set; }

        public Card(CardDTO Item)
        {
            Id = Item.Id;
            Name = Item.Name;
            MultiverseId = Item.MultiverseId;
            ManaCost = Item.ManaCost;
            Cmc = Item.Cmc;
            Type = Item.Type;
            Rarity = Item.Rarity;
            SetId = Item.SetCode;
            Text = Item.Text;
            ImageUrl = Item.ImageUrl;
            Artist = Item.Artist;
            Power = Item.Power;
            Toughness = Item.Toughness;
            Layout = Item.Layout;
    }

        public Card()
        {
        }

		public void UpdateFromDTO(CardDTO Item)
		{
            Name = Item.Name;
            MultiverseId = Item.MultiverseId;
            ManaCost = Item.ManaCost;
            Cmc = Item.Cmc;
            Type = Item.Type;
            Rarity = Item.Rarity;
            SetId = Item.SetCode;
            Text = Item.Text;
            ImageUrl = Item.ImageUrl;
            Artist = Item.Artist;
            Power = Item.Power;
            Toughness = Item.Toughness;
            Layout = Item.Layout;
		}
        
    }

    public class CardColor
    {
        public string CardId { get; set; }
        public string Color { get; set; }

        public CardColor()
        {
        }
    }
}