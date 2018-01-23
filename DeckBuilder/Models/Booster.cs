using System.ComponentModel.DataAnnotations;

namespace DeckBuilder.Models
{
    public class Booster
    {
        [Key]
        public Set Set { get; set; }
        [Key]
        public string Card { get; set; }
    }
}
