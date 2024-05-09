using Newtonsoft.Json;

namespace MobileVRNetwork.InitialConnection
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