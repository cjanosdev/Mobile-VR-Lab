using System;
using Newtonsoft.Json;
using Model.Messages;

namespace Network.Messages.Transparency
{
    public class TransparencyData : MessageBase
    {
        [JsonProperty("transparency")]
        
        public Boolean EnableTransparencyMode { get; set; }
        
    }
}