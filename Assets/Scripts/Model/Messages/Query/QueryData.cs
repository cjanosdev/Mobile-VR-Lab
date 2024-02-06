namespace Model.Messages.Query
{
    public class QueryData
    {
        /// <summary>
        ///     When true indicates the headset is turned on and "active"
        ///     otherwise false means the headset is turned off or "inactive"
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        ///     Provides the battery percentage for the headset. 
        /// </summary>
        public double BatteryLevel { get; set; }
    }
}