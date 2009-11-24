// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdentityConstraintException.cs">
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
//   This exception is used to report identity constraint violations.
//   Assigning an item identifier, a subject identifier, or a subject locator to
//   different objects causes an <c>IdentityConstraintException</c> to be thrown.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
        /// <param name="reporter">
        /// The <see cref="IConstruct"/> to which the identity should have been assigned to. 
        /// In case a factory method has thrown this exception, the construct which provides the factory method.
        /// </param>
        /// <param name="existing">
        /// The <see cref="IConstruct"/> which has the same identity.
        /// </param>
        /// <param name="locator">
        /// The <see cref="ILocator"/> representing the identity.
        /// </param>
        /// <param name="message">
        /// The detail message.
        /// </param>
        public IdentityConstraintException(IConstruct reporter, IConstruct existing, ILocator locator, string message)
            : base(reporter, message)
        {
            Existing = existing;
            Locator = locator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityConstraintException"/> class that wraps another exception with the specified detail <paramref name="message"/>.
        /// </summary>
        /// <param name="reporter">
        /// The <see cref="IConstruct"/> to which the identity should have been assigned to. 
        /// In case a factory method has thrown this exception, the construct which provides the factory method.
        /// </param>
        /// <param name="existing">
        /// The <see cref="IConstruct"/> which has the same identity.
        /// </param>
        /// <param name="locator">
        /// The <see cref="ILocator"/> representing the identity.
        /// </param>
        /// <param name="message">
        /// The detail message.
        /// </param>
        /// <param name="innerException">
        /// Exception to be wrapped.
        /// </param>
        public IdentityConstraintException(IConstruct reporter, IConstruct existing, ILocator locator, string message, Exception innerException)
            : base(reporter, message, innerException)
        {
            Existing = existing;
            Locator = locator;
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

        #region Properties

        /// <summary>
        /// Gets the <see cref="IConstruct"/> which already has the identity represented by the locator <see cref="Locator"/>.
        /// </summary>
        public IConstruct Existing { get; private set; }

        /// <summary>
        /// Gets the <see cref="ILocator"/> representing the identity that caused the exception.
        /// </summary>
        public ILocator Locator { get; private set; }

        #endregion
    }
}