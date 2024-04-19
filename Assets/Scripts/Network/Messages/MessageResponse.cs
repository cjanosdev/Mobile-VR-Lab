using Newtonsoft.Json;

namespace Model.Messages
{
    public class MessageResponse
    {
            /// <summary>
            /// If true indicates there is an error
            /// in fufilling the message command.
            /// </summary>
            [JsonProperty("error")]
            public bool Error { get; set; }
        
            /// <summary>
            /// TODO: This should really be of type T
            /// or something to send a dictionary back
            /// but need time to implement.
            /// </summary>
            [JsonProperty("response")]
            public string Response { get; set;}
    }
}