using System.Collections.Generic;
using RestSharp.Deserializers;


namespace DeckBuilder.DTO
{
    public class CardListDTO
    {
        [DeserializeAs(Name = "cards")]
        public List<CardDTO> Cards { get; set; }
    }
}
