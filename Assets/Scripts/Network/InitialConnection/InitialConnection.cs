using Newtonsoft.Json;

namespace Model.InitialConnection
{
    public class InitialConnection
    {
        /// <summary>
        ///     The protocol version being used by the server.
        /// </summary>
        [JsonProperty("version")]
        public int Version { get; set; }
    }
}