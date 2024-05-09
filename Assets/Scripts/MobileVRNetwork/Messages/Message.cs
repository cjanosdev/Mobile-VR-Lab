
using Newtonsoft.Json;

namespace MobileVRNetwork.Messages
{
    public class Message<T> : MessageBase
    {
        
        /// <summary>
        ///     The message data.
        /// </summary>
        [JsonProperty("kwargs")]
        public T Data { get; set; }
    }
}