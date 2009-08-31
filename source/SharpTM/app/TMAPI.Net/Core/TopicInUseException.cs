namespace TMAPI.Net.Core
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Thrown when an attempt is made to remove a <see cref="T:TMAPI.Net.Core.ITopic"/> which is being used as 
    /// a type, as a reifier, or as a role player in an association, or in a scope.
    /// </summary>
    [Serializable]
    public class TopicInUseException : ModelConstraintException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicInUseException"/> class.
        /// </summary>
        public TopicInUseException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicInUseException"/> class with the specified detail <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The detail message.
        /// </param>
        public TopicInUseException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicInUseException"/> class that wraps another exception with the specified detail <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The detail message.
        /// </param>
        /// <param name="innerException">
        /// Exception to be wrapped.
        /// </param>
        public TopicInUseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicInUseException"/> class with serialized data.
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
        protected TopicInUseException(
                SerializationInfo info,
                StreamingContext context) : base(info, context)
        {
        }

        #endregion
    }
}