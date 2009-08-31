namespace TMAPI.Net.Core
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// A generic interface to a TMAPI system.
    /// Any TMAPI system must be capable of providing access to one or more 
    /// <see cref="T:TMAPI.Net.Core.ITopicMap"/>objects. A TMAPI system may be capable of 
    /// allowing a client to create new <see cref="T:TMAPI.Net.Core.ITopicMap"/> instances.
    /// </summary>
    public interface ITopicMapSystem
    {
        #region Properties

        /// <summary>
        /// Gets all storage addresses of <see cref="T:TMAPI.Net.Core.ITopicMap"/> instances 
        /// known by this system.
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <returns>
        /// An unmodifiable set of <see cref="T:TMAPI.Net.Core.ILocator"/>s which represent IRIs of known 
        /// <see cref="T:TMAPI.Net.Core.ITopicMap"/> instances.
        /// </returns>
        ReadOnlyCollection<ILocator> Locators
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applications SHOULD call this method when the TopicMapSystem instance is no longer required.
        /// Once the TopicMapSystem instance is closed, the TopicMapSystem and any object retrieved from 
        /// or created in this TopicMapSystem MUST NOT be used by the application.
        /// An implementation of the TopicMapSystem interface may use this method to clean up any 
        /// resources used by the implementation.
        /// </summary>
        void Close();

        /// <summary>
        /// Returns a <see cref="T:TMAPI.Net.Core.ILocator"/> instance representing the specified IRI 
        /// <paramref name="reference"/>. The specified IRI <paramref name="reference"/> is assumed to be absolute.
        /// </summary>
        /// <param name="reference">
        /// A string which uses the IRI notation.
        /// </param>
        /// <returns>
        /// A <see cref="T:TMAPI.Net.Core.ILocator"/> representing the IRI <paramref name="reference"/>.
        /// </returns>
        ILocator CreateLocator(string reference);

        /// <summary>
        /// Creates a new <see cref="T:TMAPI.Net.Core.ITopicMap"/> and stores it within the system under the 
        /// specified <paramref name="iri"/>.
        /// </summary>
        /// <param name="iri">
        /// The address which should be used to store the <see cref="T:TMAPI.Net.Core.ITopicMap"/>.
        /// <seealso href="http://www.ietf.org/rfc/rfc3987.txt">RFC: Internationalized Resource Identifiers (IRIs)</seealso>
        /// </param>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.ITopicMap"/> instance.
        /// </returns>
        /// <exception cref="TopicMapExistsException">
        /// If this <see cref="ITopicMapSystem"/> already manages a <see cref="ITopicMap"/> under the specified IRI.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
                "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "iri",
                Justification = "Means the Internationalized Resource Identifier (IRI)")]
        ITopicMap CreateTopicMap(ILocator iri);

        /// <summary>
        /// Creates a new <see cref="T:TMAPI.Net.Core.ITopicMap"/> and stores it within the system under the 
        /// specified <paramref name="iri"/>. The string is assumed to be in IRI notation.
        /// </summary>
        /// <param name="iri">
        /// The address which should be used to store the <see cref="T:TMAPI.Net.Core.ITopicMap"/>.
        /// <seealso href="http://www.ietf.org/rfc/rfc3987.txt">RFC: Internationalized Resource Identifiers (IRIs)</seealso>
        /// </param>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.ITopicMap"/> instance.
        /// </returns>
        /// <exception cref="TopicMapExistsException">
        /// If this <see cref="ITopicMapSystem"/> already manages a <see cref="ITopicMap"/> under the specified IRI.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
                "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "iri",
                Justification = "Means the Internationalized Resource Identifier (IRI)")]
        ITopicMap CreateTopicMap(string iri);

        /// <summary>
        /// Returns a property in the underlying implementation of <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/>. 
        /// A list of the core properties defined by TMAPI can be found at 
        /// <a href="http://tmapi.org/properties/">http://tmapi.org/properties/</a>. An implementation is free to 
        /// support properties other than the core ones.
        /// The properties supported by the TopicMapSystem and the value for each property is set when the 
        /// TopicMapSystem is created by a call to <see cref="TopicMapSystemFactory.NewTopicMapSystem"/> and 
        /// cannot be modified subsequently.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property to retrieve.
        /// </param>
        /// <returns>
        /// The value set for the property or <c>null</c> if no value is set for the specified <paramref name="propertyName"/>.
        /// </returns>
        object GetProperty(string propertyName);

        /// <summary>
        /// Retrieves a <see cref="T:TMAPI.Net.Core.ITopicMap"/> managed by this system with the specified 
        /// storage address <paramref name="iri"/>. The string is assumed to be in IRI notation.
        /// </summary>
        /// <param name="iri">
        /// The storage address to retrieve the <see cref="T:TMAPI.Net.Core.ITopicMap"/> from.
        /// <seealso href="http://www.ietf.org/rfc/rfc3987.txt">RFC: Internationalized Resource Identifiers (IRIs)</seealso>
        /// </param>
        /// <returns>
        /// The <see cref="T:TMAPI.Net.Core.ITopicMap"/> instance managed by this system which is stored at the 
        /// specified <paramref name="iri"/>, or <c>null</c> if no such <see cref="T:TMAPI.Net.Core.ITopicMap"/> is found.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
                "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "iri",
                Justification = "Means the Internationalized Resource Identifier (IRI)")]
        ITopicMap GetTopicMap(string iri);

        /// <summary>
        /// Retrieves a <see cref="T:TMAPI.Net.Core.ITopicMap"/> managed by this system with the specified 
        /// storage address <paramref name="iri"/>.
        /// </summary>
        /// <param name="iri">
        /// The storage address to retrieve the <see cref="T:TMAPI.Net.Core.ITopicMap"/> from.
        /// <seealso href="http://www.ietf.org/rfc/rfc3987.txt">RFC: Internationalized Resource Identifiers (IRIs)</seealso>
        /// </param>
        /// <returns>
        /// The <see cref="T:TMAPI.Net.Core.ITopicMap"/> instance managed by this system which is stored at the 
        /// pecified <paramref name="iri"/>, or <c>null</c> if no such <see cref="T:TMAPI.Net.Core.ITopicMap"/> is found.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
                "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "iri",
                Justification = "Means the Internationalized Resource Identifier (IRI)")]
        ITopicMap GetTopicMap(ILocator iri);

        /// <summary>
        /// Returns the value of the feature specified by <paramref name="featureName"/> for this 
        /// TopicMapSystem instance.
        /// The features supported by the TopicMapSystem and the value for each feature is set when the 
        /// TopicMapSystem is created by a call to <see cref="TopicMapSystemFactory.NewTopicMapSystem"/> and 
        /// cannot be modified subsequently.
        /// </summary>
        /// <param name="featureName">
        /// The name of the feature to check.
        /// </param>
        /// <returns>
        /// <c>true</c>if the named feature is enabled this TopicMapSystem instance; 
        /// <c>false</c>if the named feature is disabled for this instance.
        /// </returns>
        bool GetFeature(string featureName);

        #endregion
    }
}