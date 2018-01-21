using System.Collections.Generic;
using System.Runtime.Serialization;


namespace DeckBuilder.DTO
{
    public class CardListDTO
    {
        [DataMember(Name = "cards")]
        public List<CardDTO> Cards { get; set; }
    }
}
