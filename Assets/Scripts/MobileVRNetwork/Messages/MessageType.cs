namespace MobileVRNetwork.Messages
{
    public enum MessageType
    {
        /// <summary>
        /// This message queries the headset for status type information
        /// (active/inactive, battery level, etc.)
        /// </summary>
        Query = 0,
        
        /// <summary>
        ///     This message instructs the headset to change to a given scene.
        /// </summary>
        ChangeScene = 1,
        
        /// <summary>
        ///     This message instructs the headset to focus
        ///     on an object.
        /// </summary>
        Focus = 2,
        
        /// <summary>
        ///     This message instructs the headset to set or unset attention mode.
        /// </summary>
        AttentionMode = 3,
        
        /// <summary>
        ///     This message instructs the headset to set or unset passthrough mode.
        /// </summary>
        TransparencyMode = 4,
        
        
    }
}