using Newtonsoft.Json;

namespace Model.Messages.Query
{
    public class QueryData : MessageBase
    {
        /// <summary>
        ///     Query the headset for information.
        /// </summary>
        [JsonProperty("query")]
        public HeadsetStatus Query { get; set; }
    }
}