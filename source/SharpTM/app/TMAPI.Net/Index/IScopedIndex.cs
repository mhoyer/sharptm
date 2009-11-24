// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IScopedIndex.cs">
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
//   Index for <see cref="T:TMAPI.Net.Core.IScoped" /> statements and their scope.
//   This index provides access to <see cref="T:TMAPI.Net.Core.IAssociation" />s,
//   <see cref="T:TMAPI.Net.Core.IOccurrence" />s, <see cref="T:TMAPI.Net.Core.IName" />s,
//   and <see cref="T:TMAPI.Net.Core.IVariant" />s by their scope property and to
//   <see cref="T:TMAPI.Net.Core.ITopic" />s which are used as theme in a scope.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.Index
{
    using System.Collections.ObjectModel;
    using Core;

    /// <summary>
    /// Index for <see cref="T:TMAPI.Net.Core.IScoped"/> statements and their scope. 
    /// This index provides access to <see cref="T:TMAPI.Net.Core.IAssociation"/>s, 
    /// <see cref="T:TMAPI.Net.Core.IOccurrence"/>s, <see cref="T:TMAPI.Net.Core.IName"/>s, 
    /// and <see cref="T:TMAPI.Net.Core.IVariant"/>s by their scope property and to 
    /// <see cref="T:TMAPI.Net.Core.ITopic"/>s which are used as theme in a scope.
    /// </summary>
    public interface IScopedIndex : IIndex
    {
        #region Properties

        /// <summary>
        /// Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s in the topic map used 
        /// in the scope property of <see cref="T:TMAPI.Net.Core.IAssociation"/>s. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <returns>
        ///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
        /// </returns>
        ReadOnlyCollection<ITopic> AssociationThemes
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s in the topic map used 
        /// in the scope property of <see cref="T:TMAPI.Net.Core.IName"/>s. 
        /// The return value may be empty but must never be <tt>null</tt>.
        /// </summary>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
        /// </returns>
        ReadOnlyCollection<ITopic> NameThemes
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s in the topic map used 
        /// in the scope property of <see cref="T:TMAPI.Net.Core.IOccurrence"/>s. 
        /// The return value may be empty but must never be <tt>null</tt>.
        /// </summary>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
        /// </returns>
        ReadOnlyCollection<ITopic> OccurrenceThemes
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s in the topic map used 
        /// in the scope property of <see cref="T:TMAPI.Net.Core.IVariant"/>s. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
        /// </returns>
        ReadOnlyCollection<ITopic> VariantThemes
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IAssociation"/>s in the topic map whose 
        /// scope property contains the specified <paramref name="theme"/>. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="theme">
        /// <see cref="T:TMAPI.Net.Core.ITopic"/>which must be part of the scope. 
        /// If it is <c>null</c> all <see cref="T:TMAPI.Net.Core.IAssociation"/>s in the 
        /// unconstrained scope are returned.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IAssociation"/>s.
        /// </returns>
        ReadOnlyCollection<IAssociation> GetAssociations(ITopic theme);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IAssociation"/>s in the topic map whose 
        /// scope property equals one of those <paramref name="themes"/> at least. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="themes">
        /// Scope of the <see cref="T:TMAPI.Net.Core.IAssociation"/>s to be returned.
        /// </param>
        /// <param name="matchAll">
        /// If <c>true</c> the scope property of an association must match all <paramref name="themes"/>, 
        /// if <c>false</c> one theme must be matched at least.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IAssociation"/>s.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// If the <paramref name="themes"/> is <c>null</c>.
        /// </exception>
        ReadOnlyCollection<IAssociation> GetAssociations(ITopic[] themes, bool matchAll);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IName"/>s in the topic map whose 
        /// scope property contains the specified <paramref name="theme"/>. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="theme">
        /// <see cref="T:TMAPI.Net.Core.ITopic"/>which must be part of the scope. 
        /// If it is <c>null</c> all <see cref="T:TMAPI.Net.Core.IName"/>s in the 
        /// unconstrained scope are returned.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IName"/>s.
        /// </returns>
        ReadOnlyCollection<IName> GetNames(ITopic theme);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IName"/>s in the topic map whose 
        /// scope property equals one of those <paramref name="themes"/> at least. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="themes">
        /// Scope of the <see cref="T:TMAPI.Net.Core.IName"/>s to be returned.
        /// </param>
        /// <param name="matchAll">
        /// If <c>true</c> the scope property of an name must match all <paramref name="themes"/>, 
        /// if <c>false</c> one theme must be matched at least.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IName"/>s.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// If the <paramref name="themes"/> is <c>null</c>.
        /// </exception>
        ReadOnlyCollection<IName> GetNames(ITopic[] themes, bool matchAll);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s in the topic map whose 
        /// scope property contains the specified <paramref name="theme"/>. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="theme">
        /// <see cref="T:TMAPI.Net.Core.ITopic"/>which must be part of the scope. 
        /// If it is <c>null</c> all <see cref="T:TMAPI.Net.Core.IOccurrence"/>s in the 
        /// unconstrained scope are returned.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IOccurrence"/>s.
        /// </returns>
        ReadOnlyCollection<IOccurrence> GetOccurrences(ITopic theme);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s in the topic map whose 
        /// scope property equals one of those <paramref name="themes"/> at least. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="themes">
        /// Scope of the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s to be returned.
        /// </param>
        /// <param name="matchAll">
        /// If <c>true</c> the scope property of an occurrence must match all <paramref name="themes"/>, 
        /// if <c>false</c> one theme must be matched at least.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IOccurrence"/>s.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// If the <paramref name="themes"/> is <c>null</c>.
        /// </exception>
        ReadOnlyCollection<IOccurrence> GetOccurrences(ITopic[] themes, bool matchAll);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IVariant"/>s in the topic map whose 
        /// scope property contains the specified <paramref name="theme"/>. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="theme">
        /// <see cref="T:TMAPI.Net.Core.ITopic"/>which must be part of the scope. 
        /// This must not be null <c>null</c>.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IVariant"/>s.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// If the <paramref name="theme"/> is <c>null</c>.
        /// </exception>
        ReadOnlyCollection<IVariant> GetVariants(ITopic theme);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IVariant"/>s in the topic map whose 
        /// scope property equals one of those <paramref name="themes"/> at least. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="themes">
        /// Scope of the <see cref="T:TMAPI.Net.Core.IVariant"/>s to be returned.
        /// </param>
        /// <param name="matchAll">
        /// If <c>true</c> the scope property of an variant must match all <paramref name="themes"/>, 
        /// if <c>false</c> one theme must be matched at least.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IOccurrence"/>s.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// If the <paramref name="themes"/> is <c>null</c>.
        /// </exception>
        ReadOnlyCollection<IVariant> GetVariants(ITopic[] themes, bool matchAll);

        #endregion
    }
}