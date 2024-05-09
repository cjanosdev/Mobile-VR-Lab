using System;
using Newtonsoft.Json;


namespace MobileVRNetwork.Messages.Transparency
{
    public class TransparencyData : MessageBase
    {
        [JsonProperty("transparency")]
        
        public Boolean EnableTransparencyMode { get; set; }
        
    }
}