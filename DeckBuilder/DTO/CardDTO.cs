using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DeckBuilder.DTO
{
    public class CardDTO
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
