using Model.Messages;
using Newtonsoft.Json;

namespace Model.Focus
{
    public class FocusData : MessageBase
    {
        /// <summary>
        /// The ID of the game object to focus on in the scene.
        /// </summary>
        [JsonProperty("object")]
        public int ObjectID { get; set; }
    }
}