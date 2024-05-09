
using Newtonsoft.Json;

namespace MobileVRNetwork.Messages.Focus
{
    public class FocusData : MessageBase
    {
        /// <summary>
        /// The ID of the game object to focus on in the scene.
        /// </summary>
        [JsonProperty("object")]
        public string ObjectID { get; set; }
    }
}