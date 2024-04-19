using Newtonsoft.Json;

namespace Model.InitialConnection
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