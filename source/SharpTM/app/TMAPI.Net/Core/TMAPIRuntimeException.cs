namespace TMAPI.Net.Core
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Instances of this exception class should be thrown in cases where there is an 
    /// error in the underlying topic map processing system or when integrity constraints are violated.
    /// </summary>
    [Serializable]
    public class TMAPIRuntimeException : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TMAPIRuntimeException"/> class.
        /// </summary>
        public TMAPIRuntimeException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TMAPIRuntimeException"/> class with the specified detail <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The detail message.
        /// </param>
        public TMAPIRuntimeException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TMAPIRuntimeException"/> class that wraps another exception with the specified detail <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The detail message.
        /// </param>
        /// <param name="innerException">
        /// Exception to be wrapped.
        /// </param>
        public TMAPIRuntimeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TMAPIRuntimeException"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object
        /// data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual
        /// information about the source or destination.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is null. 
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). 
        /// </exception>
        protected TMAPIRuntimeException(
                SerializationInfo info,
                StreamingContext context) : base(info, context)
        {
        }

        #endregion
    }
}