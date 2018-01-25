using System;
using System.Collections.Generic;
using RestSharp.Deserializers;

namespace DeckBuilder.DTO
{
    public class SetDTO
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
        
        [DataMember(Name = "border")]
        public string Border { get; set; }

        [DataMember(Name = "booster")]
        public List<string> Booster { get; set; }

        [DataMember(Name = "releaseDate")]
        public DateTime ReleaseDate { get; set; }
    }

    public class SetListDTO
    {
        public List<SetDTO> Sets { get; set; }
    }
}
