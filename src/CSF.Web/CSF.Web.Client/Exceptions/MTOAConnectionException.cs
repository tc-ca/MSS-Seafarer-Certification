namespace CSF.Web.Client.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines an exception when trying to connect to the MTOA service.
    /// </summary>
    [Serializable]
    public class MtoaConnectionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MtoaConnectionException"/> class.
        /// </summary>
        public MtoaConnectionException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MtoaConnectionException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public MtoaConnectionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MtoaConnectionException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public MtoaConnectionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MtoaConnectionException"/> class.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected MtoaConnectionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
