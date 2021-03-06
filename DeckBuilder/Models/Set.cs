using System;
using DeckBuilder.DTO;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DeckBuilder.Models
{
    public class Set
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Border { get; set; }

		public ICollection<Card> Cards { get; set; }

		public Set(SetDTO Item)
        {
            Code = Item.Code;
            Name = Item.Name;
            Type = Item.Type;
            Border = Item.Border;
            ReleaseDate = Item.ReleaseDate;
        }

        public Set()
        {
        }

		public void UpdateFromDTO(SetDTO Item)
		{
            Name = Item.Name;
            Type = Item.Type;
            Border = Item.Border;
            ReleaseDate = Item.ReleaseDate;
		}
    }
}