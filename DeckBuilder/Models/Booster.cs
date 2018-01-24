using System.ComponentModel.DataAnnotations;

namespace DeckBuilder.Models
{
    public class Booster
    {
        public string SetCode { get; set; }
        public string CardType { get; set; }

		public Booster()
		{
		}

		public Booster(string Code, string Id)
		{
			SetCode = Code;
			CardType = Id;
		}
    }

}
