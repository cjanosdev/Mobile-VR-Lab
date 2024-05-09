using Newtonsoft.Json;

namespace MobileVRNetwork.InitialConnection
{
    public class InitialResponse
    {
        /// <summary>
        ///     The headset's unique identifier (probably will eventually use uuid).
        /// </summary>
         [JsonProperty("id")]
        public string ID { get; set; }
    }
}