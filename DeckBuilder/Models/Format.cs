using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeckBuilder.Models
{
    public class Format
    {
		[Key]
		public string Name { get; set; }
	}
}
