using System;
using System.Runtime.Serialization;

namespace DefaultNamespace
{
    /// <summary>
    ///     Represents errors while validating
    /// </summary>
    public sealed class MobileVRLabValidationException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the Mobile Vr Lab Validation Exception
        ///     class.
        /// </summary>
        public MobileVRLabValidationException()
        {
            
        }
        
        /// <summary>
        ///     Initializes a new instance of the Mobile VR Lab
        ///     Exceptions.
        /// </summary>
        /// <param name="message"> The message that describes the error</param>
        public MobileVRLabValidationException(string message) : base(message)
        {
            
        }

        /// <summary>
        ///     Initializes a new instance of the Mobile Vr Lab
        ///     Class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public MobileVRLabValidationException(string message, Exception innerException)
        {
            
        }
        
        private MobileVRLabValidationException(SerializationInfo info, StreamingContext context): base(info, context)
        {
            
        }
        
        
    }
}