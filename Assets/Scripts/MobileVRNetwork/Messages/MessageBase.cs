using Newtonsoft.Json;

namespace MobileVRNetwork.Messages
{
    public class MessageBase
    {
        /// <summary>
        /// The message type <see cref="MessageType"/>
        /// </summary>
        [JsonProperty("id")]
        public MessageType MessageType { get; set; }
    }
}