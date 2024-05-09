
using Newtonsoft.Json;

namespace MobileVRNetwork.Messages.ChangeScene
{
    public class ChangeSceneData : MessageBase
    {
        /// <summary>
        ///     The ID used in the XML Configuration
        ///     for the scene.
        /// </summary>
        [JsonProperty("scene")]
        public int SceneID { get; set; }
    }
}