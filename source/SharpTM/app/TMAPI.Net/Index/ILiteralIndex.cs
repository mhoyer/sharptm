// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILiteralIndex.cs">
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
//   Index for literal values stored in a topic map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.Index
{
    using System.Collections.ObjectModel;

    using TMAPI.Net.Core;

    /// <summary>
    /// Index for literal values stored in a topic map.
    /// </summary>
    public interface ILiteralIndex : IIndex
    {
        #region Methods

        /// <summary>
        /// Retrieves the <see cref="T:TMAPI.Net.Core.IName"/>s in the topic map which have a value equal to <paramref name="value"/>.
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="value">
        /// The value of the <see cref="T:TMAPI.Net.Core.IName"/>s to be returned.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IName"/>s.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// If the <paramref name="value"/> is <c>null</c>.
        /// </exception>
        ReadOnlyCollection<IName> GetNames(string value);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s in the topic map 
        /// whose value property matches <paramref name="value"/> and whose datatype 
        /// property is <a href="http://www.w3.org/TR/xmlschema-2/#string">xsd:string</a>. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="value">
        /// The value of the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s to be returned.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IOccurrence"/>s.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// If the <paramref name="value"/> is <c>null</c>.
        /// </exception>
        ReadOnlyCollection<IOccurrence> GetOccurrences(string value);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s in the topic map 
        /// whose value property matches the IRI represented by <paramref name="value"/>. 
        /// Those <see cref="T:TMAPI.Net.Core.IOccurrence"/>s which have a datatype equal to 
        /// <a href="http://www.w3.org/TR/xmlschema-2/#anyURI">xsd:anyURI</a>and their 
        /// value property is equal to <see cref="P:TMAPI.Net.Core.ILocator.Reference"/> are returned. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="value">
        /// The value of the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s to be returned.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IOccurrence"/>s.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// If the <paramref name="value"/> is <c>null</c>.
        /// </exception>
        ReadOnlyCollection<IOccurrence> GetOccurrences(ILocator value);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s in the topic map 
        /// whose value property matches <paramref name="value"/> and whose <c>datatype</c> is <paramref name="datatype"/>. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="value">
        /// The value of the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s to be returned.
        /// </param>
        /// <param name="datatype">
        /// The <c>datatype</c> of the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s to be returned.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IOccurrence"/>s.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// If the <paramref name="value"/> or <paramref name="datatype"/> is <c>null</c>.
        /// </exception>
        ReadOnlyCollection<IOccurrence> GetOccurrences(string value, ILocator datatype);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IVariant"/>s in the topic map whose 
        /// value property matches <paramref name="value"/> and whose <c>datatype</c> property is 
        /// <a href="http://www.w3.org/TR/xmlschema-2/#string"><c>xsd:string</c></a>.
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="value">
        /// The value of the <see cref="T:TMAPI.Net.Core.IVariant"/>s to be returned.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IVariant"/>s.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// If the <paramref name="value"/> is <c>null</c>.
        /// </exception>
        ReadOnlyCollection<IVariant> GetVariants(string value);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IVariant"/>s in the topic map whose 
        /// value property matches the IRI represented by <paramref name="value"/>. 
        /// Those <see cref="T:TMAPI.Net.Core.IVariant"/>s which have a <c>datatype</c> equal to 
        /// <a href="http://www.w3.org/TR/xmlschema-2/#anyURI"><c>xsd:anyURI</c></a>and their 
        /// value property is equal to <see cref="P:TMAPI.Net.Core.ILocator.Reference"/> are returned. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="value">
        /// The value of the <see cref="T:TMAPI.Net.Core.IVariant"/>s to be returned.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IVariant"/>s.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// If the <paramref name="value"/> is <c>null</c>.
        /// </exception>
        ReadOnlyCollection<IVariant> GetVariants(ILocator value);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IVariant"/>s in the topic map 
        /// whose value property matches <paramref name="value"/> and whose <c>datatype</c> is <paramref name="datatype"/>. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="value">
        /// The value of the <see cref="T:TMAPI.Net.Core.IVariant"/>s to be returned.
        /// </param>
        /// <param name="datatype">
        /// The <c>datatype</c> of the <see cref="T:TMAPI.Net.Core.IVariant"/>s to be returned.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IVariant"/>s.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// If the <paramref name="value"/> or <paramref name="datatype"/> is <c>null</c>.
        /// </exception>
        ReadOnlyCollection<IVariant> GetVariants(string value, ILocator datatype);

        #endregion
    }
}