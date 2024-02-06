using System.Collections.Generic;
using Model;

namespace Modules
{
    public class Command<T>
    {
        /// <summary>
        /// The ID of the Message.
        /// </summary>
        public int MessageID { get; set; }

        /// <summary>
        ///     The key word arguments for the message.
        /// </summary>
        public Dictionary<string, Message<T>> Kwargs { get; set; }

    }
}