namespace DefaultNamespace
{
    public static class ValidationExtensions
    {
        /// <summary>
        ///     Validates if a parameter is null and throws MobileVRLabValidationException
        ///     if so.
        /// </summary>
        /// <param name="parameter"> Parameter to be validated.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="MobileVRLabValidationException">Exception to be thrown.</exception>
        public static void ShouldNotBeNull(this object parameter, string parameterName)
        {
            if (parameter == null)
            {
                throw new MobileVRLabValidationException("Value cannot be null. \r\nParameter name: " + parameterName);
            }
        }
        
        /// <summary>
        ///     Validates that a parameter is not null, empty parameter or just whitespace
        ///     and throws MobileVRLabValidationException if so.
        /// </summary>
        /// <param name="parameter"> Parameter to be validated.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="MobileVRLabValidationException">Exception to be thrown.</exception>
        public static void ShouldNotBeNull(this string parameter, string parameterName)
        {
            if (string.IsNullOrWhiteSpace((parameter)))
            {
                throw new MobileVRLabValidationException("Value cannot be null or whitespace. \r\nParameter name: " +
                                                         parameterName);
            }
        }
    }
}