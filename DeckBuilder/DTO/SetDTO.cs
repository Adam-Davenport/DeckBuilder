using System;
using System.Collections.Generic;
using RestSharp.Deserializers;

namespace DeckBuilder.DTO
{
    public class SetDTO
    {
        [DeserializeAs(Name = "code")]
        public string Code { get; set; }

        [DeserializeAs(Name = "name")]
        public string Name { get; set; }

        [DeserializeAs(Name = "type")]
        public string Type { get; set; }
        
        [DeserializeAs(Name = "border")]
        public string Border { get; set; }

        [DeserializeAs(Name = "booster")]
        public List<string> Booster { get; set; }

        [DeserializeAs(Name = "releaseDate")]
        public DateTime ReleaseDate { get; set; }
    }

    public class SetListDTO
    {
        public List<SetDTO> Sets { get; set; }
    }
}
