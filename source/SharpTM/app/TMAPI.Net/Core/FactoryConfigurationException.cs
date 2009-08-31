namespace TMAPI.Net.Core
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception thrown when a <see cref="T:TMAPI.Net.Core.TopicMapSystemFactory"/> instance cannot be 
    /// instantiated through the method <see cref="M:TMAPI.Net.Core.TopicMapSystemFactory.NewInstance"/>.
    /// </summary>
    [Serializable]
    public class FactoryConfigurationException : TMAPIException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FactoryConfigurationException"/> class.
        /// </summary>
        public FactoryConfigurationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FactoryConfigurationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The detail message.
        /// </param>
        public FactoryConfigurationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FactoryConfigurationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The detail message.
        /// </param>
        /// <param name="innerException">
        /// Exception to be wrapped.
        /// </param>
        public FactoryConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FactoryConfigurationException"/> class.
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
        protected FactoryConfigurationException(
                SerializationInfo info,
                StreamingContext context) : base(info, context)
        {
        }

        #endregion
    }
}