// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelConstraintException.cs">
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
//   This exception is used to report
//   <a href="http://www.isotopicmaps.org/sam/sam-model/">Topic Maps — Data Model</a>constraint violations.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.Core
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// This exception is used to report 
    /// <a href="http://www.isotopicmaps.org/sam/sam-model/">Topic Maps — Data Model</a>constraint violations.
    /// </summary>
    [Serializable]
    public class ModelConstraintException : TMAPIRuntimeException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelConstraintException"/> class.
        /// </summary>
        /// <param name="reporter">
        /// The <see cref="IConstruct"/> which has thrown this exception.
        /// </param>
        /// <param name="message">
        /// The detail message.
        /// </param>
        public ModelConstraintException(IConstruct reporter, string message)
            : base(message)
        {
            Reporter = reporter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelConstraintException"/> class that wraps another exception with the specified detail <paramref name="message"/>.
        /// </summary>
        /// <param name="reporter">
        /// The <see cref="IConstruct"/> which has thrown this exception.
        /// </param>
        /// <param name="message">
        /// The detail message.
        /// </param>
        /// <param name="innerException">
        /// Exception to be wrapped.
        /// </param>
        public ModelConstraintException(IConstruct reporter, string message, Exception innerException)
            : base(message, innerException)
        {
            Reporter = reporter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelConstraintException"/> class with serialized data.
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
        protected ModelConstraintException(
                SerializationInfo info,
                StreamingContext context) : base(info, context)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the <see cref="IConstruct"/> which has thrown the exception.
        /// </summary>
        public IConstruct Reporter { get; private set; }

        #endregion
    }
}