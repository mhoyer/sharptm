namespace TMAPI.Net.Core
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception thrown when an attempt is made to create a new <see cref="T:TMAPI.Net.Core.ITopicMap"/> under 
    /// a storage address (an IRI) that is already assigned to another <see cref="T:TMAPI.Net.Core.ITopicMap"/> 
    /// in the same <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/>.
    /// </summary>
    [Serializable]
    public class TopicMapExistsException : TMAPIException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicMapExistsException"/> class.
        /// </summary>
        public TopicMapExistsException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicMapExistsException"/> class with the specified detail <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The detail message.
        /// </param>
        public TopicMapExistsException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicMapExistsException"/> class that wraps another exception with the specified detail <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The detail message.
        /// </param>
        /// <param name="innerException">
        /// Exception to be wrapped.
        /// </param>
        public TopicMapExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicMapExistsException"/> class with serialized data.
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
        protected TopicMapExistsException(
                SerializationInfo info,
                StreamingContext context) : base(info, context)
        {
        }

        #endregion
    }
}