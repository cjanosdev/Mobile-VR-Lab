using System.Collections.Generic;

namespace Model
{
    public class Message<T>
    {
        /// <summary>
        /// The message type <see cref="MessageType"/>
        /// </summary>
        public MessageType MessageType { get; set; }
        
        /// <summary>
        ///     The message data.
        /// </summary>
        public T Data { get; set; }
    }
}