using System.Runtime.Serialization;

namespace DeckBuilder.Models
{
    [DataContract(Name="card")]
    public class Card
    {
        [DataMember(Name="id")]
        public string Id { get; set; } // Unique id of the card for a specific set

        [DataMember(Name="multiverseid")]
        public int MultiverseId { get; set; } // Multiverse id of the card

        [DataMember(Name="name")]
        public string Name { get; set; }
        // public Set Set { get; set; }
        // public int Number { get; set; }
        // public string ManaCost { get; set; }
        // public int Cmc { get; set; }
        // public int Power { get; set; }
        // public int Toughness { get; set; }
        // public string Type { get; set; }
        // public string Subtype { get; set; }
        // public string Image { get; set; }
        // public string Rarity { get; set; }
        // public string Text { get; set; }
        // public string FlavorText { get; set; }
        // public string Artist { get; set; }
        // public string Layout { get; set; }
        
    }
}