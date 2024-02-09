using System.Collections.Generic;
using Model.Messages;
using Newtonsoft.Json;

namespace Model
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