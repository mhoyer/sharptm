namespace TMAPI.Net.Core
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// This exception is used to report identity constraint violations.
    /// Assigning an item identifier, a subject identifier, or a subject locator to 
    /// different objects causes an <c>IdentityConstraintException</c> to be thrown.
    /// </summary>
    [Serializable]
    public class IdentityConstraintException : ModelConstraintException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityConstraintException"/> class.
        /// </summary>
        public IdentityConstraintException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityConstraintException"/> class with the specified detail <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The detail message.
        /// </param>
        public IdentityConstraintException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityConstraintException"/> class that wraps another exception with the specified detail <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The detail message.
        /// </param>
        /// <param name="innerException">
        /// Exception to be wrapped.
        /// </param>
        public IdentityConstraintException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityConstraintException"/> class with serialized data.
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
        protected IdentityConstraintException(
                SerializationInfo info,
                StreamingContext context) : base(info, context)
        {
        }

        #endregion
    }
}