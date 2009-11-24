// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITypeInstanceIndex.cs">
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
//   Index for type-instance relationships between <see cref="T:TMAPI.Net.Core.ITopic" />s
//   and for <see cref="T:TMAPI.Net.Core.ITyped" /> Topic Maps constructs.
//   This index provides access to <see cref="T:TMAPI.Net.Core.ITopic" />s used in
//   <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-types">type-
//   instance</a>relationships or as type of a <see cref="T:TMAPI.Net.Core.ITyped" />
//   construct. Further, the retrieval of <see cref="T:TMAPI.Net.Core.IAssociation" />s,
//   <see cref="T:TMAPI.Net.Core.IRole" />s, <see cref="T:TMAPI.Net.Core.IOccurrence" />s,
//   and <see cref="T:TMAPI.Net.Core.IName" />s by their <c>type</c> property is supported.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.Index
{
    using System.Collections.ObjectModel;
    using Core;

    /// <summary>
    /// Index for type-instance relationships between <see cref="T:TMAPI.Net.Core.ITopic"/>s 
    /// and for <see cref="T:TMAPI.Net.Core.ITyped"/> Topic Maps constructs. 
    /// This index provides access to <see cref="T:TMAPI.Net.Core.ITopic"/>s used in
    /// <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-types">type-
    /// instance</a>relationships or as type of a <see cref="T:TMAPI.Net.Core.ITyped"/>
    /// construct. Further, the retrieval of <see cref="T:TMAPI.Net.Core.IAssociation"/>s, 
    /// <see cref="T:TMAPI.Net.Core.IRole"/>s, <see cref="T:TMAPI.Net.Core.IOccurrence"/>s, 
    /// and <see cref="T:TMAPI.Net.Core.IName"/>s by their <c>type</c> property is supported.
    /// </summary>
    public interface ITypeInstanceIndex : IIndex
    {
        #region Properties

        /// <summary>
        /// Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s in the topic map used 
        /// in the type property of <see cref="T:TMAPI.Net.Core.IAssociation"/>s. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
        /// </returns>
        ReadOnlyCollection<ITopic> AssociationTypes
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s in the topic map used 
        /// in the type property of <see cref="T:TMAPI.Net.Core.IName"/>s. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
        /// </returns>
        ReadOnlyCollection<ITopic> NameTypes
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s in the topic map used 
        /// in the type property of <see cref="T:TMAPI.Net.Core.IOccurrence"/>s. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
        /// </returns>
        ReadOnlyCollection<ITopic> OccurrenceTypes
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s in the topic map used 
        /// in the type property of <see cref="T:TMAPI.Net.Core.IRole"/>s. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
        /// </returns>
        ReadOnlyCollection<ITopic> RoleTypes
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s in topic map which are used 
        /// as type in an "type-instance"-relationship. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
        /// </returns>
        /// <remarks>
        /// Implementations may return only those topics which are member of the <c>types</c> 
        /// property of other topics and may ignore <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-types">
        /// type-instance</a> relationships which are modeled as association. 
        /// Further, <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-subtypes">supertype-subtype</a>
        /// relationships may also be ignored.
        /// </remarks>
        ReadOnlyCollection<ITopic> TopicTypes
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IAssociation"/>s in the topic map whose 
        /// type property equals <paramref name="type"/>. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="type">
        /// The type of the <see cref="T:TMAPI.Net.Core.IAssociation"/>s to be returned.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IAssociation"/>s.
        /// </returns>
        ReadOnlyCollection<IAssociation> GetAssociations(ITopic type);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IName"/>s in the topic map whose 
        /// type property equals <paramref name="type"/>. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="type">
        /// The type of the <see cref="T:TMAPI.Net.Core.IName"/>s to be returned.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IName"/>s.
        /// </returns>
        ReadOnlyCollection<IName> GetNames(ITopic type);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s in the topic map whose 
        /// type property equals <paramref name="type"/>. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="type">
        /// The type of the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s to be returned.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IOccurrence"/>s.
        /// </returns>
        ReadOnlyCollection<IOccurrence> GetOccurrences(ITopic type);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IRole"/>s in the topic map whose 
        /// type property equals <paramref name="type"/>. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="type">
        /// The type of the <see cref="T:TMAPI.Net.Core.IRole"/>s to be returned.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IRole"/>s.
        /// </returns>
        ReadOnlyCollection<IRole> GetRoles(ITopic type);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.ITopic"/>s which are an instance of 
        /// the specified <paramref name="type"/>. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="type">
        /// The type of the <see cref="T:TMAPI.Net.Core.ITopic"/>s to be returned.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
        /// </returns>
        /// <remarks>
        /// Implementations may return only those topics whose <tt>types</tt> property
        /// contains the type and may ignore <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-types">
        /// type-instance</a> relationships which are modeled as association. Further, 
        /// <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-subtypes">supertype-
        /// subtype</a>relationships may also be ignored. 
        /// </remarks>
        ReadOnlyCollection<ITopic> GetTopics(ITopic type);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.ITopic"/>s in the topic map whose type 
        /// property equals one of those <paramref name="types"/> at least. 
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="types">
        /// Types of the <see cref="T:TMAPI.Net.Core.ITopic"/>s to be returned.
        /// </param>
        /// <param name="matchAll">
        /// If <c>true</c>, a topic must be an instance of all <paramref name="types"/>, 
        /// if <c>false</c> the topic must be an instance of one type at least.
        /// </param>
        /// <returns>
        /// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
        /// </returns>
        /// <remarks>
        /// Implementations may return only those topics whose <tt>types</tt> property
        /// contains the type and may ignore <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-types">
        /// type-instance</a> relationships which are modeled as association. Further, 
        /// <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-subtypes">supertype-
        /// subtype</a>relationships may also be ignored. 
        /// </remarks>
        ReadOnlyCollection<ITopic> GetTopics(ITopic[] types, bool matchAll);

        #endregion
    }
}