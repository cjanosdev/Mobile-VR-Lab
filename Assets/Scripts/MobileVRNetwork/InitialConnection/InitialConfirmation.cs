using Newtonsoft.Json;

namespace MobileVRNetwork.InitialConnection
{
    public class InitialConfirmation
    {
            [JsonProperty("session_name")]
            public string SessionName { get; set; }
    }
}