// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILocator.cs">
//  TMAPI.Net was created collectively by the membership of the tmapinet-discuss mailing list 
//  (https://lists.sourceforge.net/lists/listinfo/tmapinet-discuss) with support by the 
//  tmapi-discuss mailing list (http://lists.sourceforge.net/mailman/listinfo/tmapi-discuss),
//  and is hereby released into the public domain; and comes with NO WARRANTY.
//  
//  No one owns TMAPI.Net: you may use it freely in both commercial and
//  non-commercial applications, bundle it with your software
//  distribution, include it on a CD-ROM, list the source code in a
//  book, mirror the documentation at your own web site, or use it in
//  any other way you see fit.
// </copyright>
// <summary>
//   Immutable representation of an IRI.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
        /// <exception cref="System.ArgumentNullException">
        /// If the <paramref name="reference"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="MalformedIRIException">
        /// If the provided string cannot be resolved against this locator.
        /// </exception>
        ILocator Resolve(string reference);

        #endregion
    }
}