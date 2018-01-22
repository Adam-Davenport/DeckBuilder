using System;
using DeckBuilder.DTO;
using System.ComponentModel.DataAnnotations;

namespace DeckBuilder.Models
{
    public class Set
    {
        [Key]
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }

        public Set(SetDTO Item)
        {
            Name = Item.Name;
            ReleaseDate = Item.ReleaseDate;
        }

        public Set()
        {
        }

    }
}