using Newtonsoft.Json;

namespace Model.InitialConnection
{
    public class InitialConfirmation
    {
            [JsonProperty("session_name")]
            public string SessionName { get; set; }
    }
}