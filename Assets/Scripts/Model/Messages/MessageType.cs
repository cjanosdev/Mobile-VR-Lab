namespace Model
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
    }
}