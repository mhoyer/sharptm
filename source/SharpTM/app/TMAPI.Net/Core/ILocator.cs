namespace TMAPI.Net.Core
{
    /// <summary>
    /// Immutable representation of an IRI.
    /// </summary>
    public interface ILocator
    {
        #region Properties

        /// <summary>
        /// Gets the external form of the IRI.
        /// Any special character will be escaped using the escaping conventions of
        /// <a href="http://www.ietf.org/rfc/rfc3987.txt">RFC 3987</a>.
        /// A string representation of this locator suitable for output or passing to 
        /// APIs which will parse the locator anew.
        /// </summary>
        string ExternalForm
        {
            get;
        }

        /// <summary>
        /// Gets a lexical representation of the IRI.
        /// </summary>
        string Reference
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns <c>true</c> if the <paramref name="other"/> object is equal to this one.
        /// To be equal the <see cref="P:TMAPI.Net.Core.ILocator.Reference"/> property of both objects must be identical. 
        /// </summary>
        /// <param name="other">
        /// The object to compare this object against.
        /// </param>
        /// <returns>
        /// Returns <c>true</c> if the two objects are equal, <c>false</c> otherwise.
        /// </returns>
        bool Equals(object other);

        /// <summary>
        /// Returns a hash code value.
        /// The returned hash code is equal to the hash code of the <see cref="P:TMAPI.Net.Core.ILocator.Reference"/> property.
        /// </summary>
        /// <returns>
        /// Hash code of the <see cref="P:TMAPI.Net.Core.ILocator.Reference"/> property.
        /// </returns>
        int GetHashCode();

        /// <summary>
        /// Resolves the <paramref name="reference"/> against this locator.
        /// The returned <c>Locator</c> represents an absolute IRI.
        /// </summary>
        /// <param name="reference">
        /// The reference which should be resolved against this locator.
        /// </param>
        /// <returns>
        /// A locator representing an absolute IRI.
        /// </returns>
        ILocator Resolve(string reference);

        #endregion
    }
}