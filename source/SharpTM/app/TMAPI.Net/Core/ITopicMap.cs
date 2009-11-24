// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITopicMap.cs">
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
//   Represents a
//   <a href="http://www.isotopicmaps.org/sam/sam-model/#d0e657">topic map item</a>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Index;

    /// <summary>
    /// Represents a 
    /// <a href="http://www.isotopicmaps.org/sam/sam-model/#d0e657">topic map item</a>.
    /// </summary>
    public interface ITopicMap : IReifiable
    {
        #region Properties

        /// <summary>
        /// Gets all <see cref="T:TMAPI.Net.Core.IAssociation"/>s contained in this topic map.
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <returns>
        /// An unmodifiable set of <see cref="T:TMAPI.Net.Core.IAssociation"/>s.
        /// </returns>
        ReadOnlyCollection<IAssociation> Associations
        {
            get;
        }

        /// <summary>
        /// Gets <c>null</c> since topic maps do not have a parent.
        /// </summary>
        /// <returns>
        /// <c>null</c> since topic maps do not have a parent.
        /// </returns>
        new IConstruct Parent
        {
            get;
        }

        /// <summary>
        /// Gets all <see cref="T:TMAPI.Net.Core.ITopic"/>s contained in this topic map.
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <returns>
        /// An unmodifiable set of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
        /// </returns>
        ReadOnlyCollection<ITopic> Topics
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Closes use of this topic map instance.
        /// This method should be invoked by the application once it is finished using this topic map instance.
        /// Implementations may release any resources required for the <c>TopicMap</c> instance or any of 
        /// the <see cref="T:TMAPI.Net.Core.IConstruct"/> instances contained by this instance.
        /// </summary>
        void Close();

        /// <summary>
        /// Creates an <see cref="T:TMAPI.Net.Core.IAssociation"/> in this topic map with the specified 
        /// <paramref name="associationType"/> and <paramref name="initialThemes"/>.
        /// </summary>
        /// <param name="associationType">
        /// The association type, MUST NOT be <c>null</c>.
        /// </param>
        /// <param name="initialThemes">
        /// An optional array of themes, MUST NOT be <c>null</c>.
        /// If the array's length is <c>0</c>, the association will be in the unconstrained scope.
        /// </param>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.IAssociation"/>.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If either the <paramref name="associationType"/> or <paramref name="initialThemes"/> is <c>null</c> or 
        /// if either the <paramref name="associationType"/> or at least one theme of <paramref name="initialThemes"/> belongs to another topic map.
        /// </exception>
        IAssociation CreateAssociation(ITopic associationType, params ITopic[] initialThemes);

        /// <summary>
        /// Creates an <see cref="T:TMAPI.Net.Core.IAssociation"/> in this topic map with the specified 
        /// <paramref name="associationType"/> and <paramref name="initialThemes"/>.
        /// </summary>
        /// <param name="associationType">
        /// The association type, MUST NOT be <c>null</c>.
        /// </param>
        /// <param name="initialThemes">
        /// A collection of themes or <c>null</c> if the association should be in the unconstrained scope.
        /// </param>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.IAssociation"/>.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If the <paramref name="associationType"/> is <c>null</c> or 
        /// if either the <paramref name="associationType"/> or at least one theme of <paramref name="initialThemes"/> belongs to another topic map.
        /// </exception>
        IAssociation CreateAssociation(ITopic associationType, IList<ITopic> initialThemes);

        /// <summary>
        /// Returns a <see cref="T:TMAPI.Net.Core.ILocator"/> instance representing the specified IRI 
        /// <paramref name="reference"/>.  
        /// The specified IRI <paramref name="reference"/> is assumed to be absolute.
        /// </summary>
        /// <param name="reference">
        /// A string which uses the IRI notation.
        /// </param>
        /// <returns>
        /// A <see cref="T:TMAPI.Net.Core.ILocator"/> representing the IRI <paramref name="reference"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="reference"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="MalformedIRIException">
        /// If the provided <paramref name="reference"/> cannot be used to create a valid <see cref="ILocator"/>.
        /// </exception>
        ILocator CreateLocator(string reference);

        /// <summary>
        /// Returns a <see cref="T:TMAPI.Net.Core.ITopic"/> instance with an automatically generated item identifier.
        /// This method returns never an existing <see cref="T:TMAPI.Net.Core.ITopic"/> but creates a new one 
        /// with an automatically generated item identifier.
        /// How that item identifier is generated depends on the implementation.
        /// </summary>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.ITopic"/> instance with an automatically generated item identifier.
        /// </returns>
        ITopic CreateTopic();

        /// <summary>
        /// Returns a <see cref="T:TMAPI.Net.Core.ITopic"/> instance with the specified item identifier.
        /// This method returns either an existing <see cref="T:TMAPI.Net.Core.ITopic"/> or creates a new 
        /// <see cref="T:TMAPI.Net.Core.ITopic"/>instance with the specified item identifier.
        /// If a topic with the specified item identifier exists in the topic map, that topic is returned. If a topic 
        /// with a subject identifier equals to the specified item identifier exists, the specified item identifier 
        /// is added to that topic and the topic is returned. If neither a topic with the specified item identifier 
        /// nor with a subject identifier equals to the item identifier exists, a topic with the item identifier is created.
        /// </summary>
        /// <param name="itemIdentifier">
        /// The item identifier the topic should contain.
        /// </param>
        /// <returns>
        /// A <see cref="T:TMAPI.Net.Core.ITopic"/> instance with the specified item identifier.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If the item identifier <paramref name="itemIdentifier"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="IdentityConstraintException">
        /// If an other <see cref="T:TMAPI.Net.Core.IConstruct"/> with the specified item identifier exists which is 
        /// not a <see cref="T:TMAPI.Net.Core.ITopic"/>. 
        /// </exception>
        ITopic CreateTopicByItemIdentifier(ILocator itemIdentifier);

        /// <summary>
        /// Returns a <see cref="T:TMAPI.Net.Core.ITopic"/> instance with the specified subject identifier.
        /// This method returns either an existing <see cref="T:TMAPI.Net.Core.ITopic"/> or creates a new 
        /// <see cref="T:TMAPI.Net.Core.ITopic"/>instance with the specified subject identifier.
        /// If a topic with the specified subject identifier exists in the topic map, that topic is returned. If a topic 
        /// with an item identifier equals to the specified subject identifier exists, the specified subject identifier 
        /// is added to that topic and the topic is returned. If neither a topic with the specified subject identifier 
        /// nor with an item identifier equals to the subject identifier exists, a topic with the subject identifier is created.
        /// </summary>
        /// <param name="subjectIdentifier">
        /// The subject identifier the topic should contain.
        /// </param>
        /// <returns>
        /// A <see cref="T:TMAPI.Net.Core.ITopic"/> instance with the specified subject identifier.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If the subject identifier <paramref name="subjectIdentifier"/> is <c>null</c>.
        /// </exception>
        ITopic CreateTopicBySubjectIdentifier(ILocator subjectIdentifier);

        /// <summary>
        /// Returns a <see cref="T:TMAPI.Net.Core.ITopic"/> instance with the specified subject locator.
        /// This method returns either an existing <see cref="T:TMAPI.Net.Core.ITopic"/> or creates a new 
        /// <see cref="T:TMAPI.Net.Core.ITopic"/>instance with the specified subject locator.
        /// </summary>
        /// <param name="subjectLocator">
        /// The subject locator the topic should contain.
        /// </param>
        /// <returns>
        /// A <see cref="T:TMAPI.Net.Core.ITopic"/> instance with the specified subject locator.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If the subject locator <paramref name="subjectLocator"/> is <c>null</c>.
        /// </exception>
        ITopic CreateTopicBySubjectLocator(ILocator subjectLocator);

        /// <summary>
        /// Returns a <see cref="T:TMAPI.Net.Core.IConstruct"/> by its (system specific) identifier.
        /// </summary>
        /// <param name="id">
        /// The identifier of the construct to be returned.
        /// </param>
        /// <returns>
        /// The construct with the specified id or <c>null</c> if such a construct is unknown.
        /// </returns>
        IConstruct GetConstructById(string id);

        /// <summary>
        /// Returns a <see cref="T:TMAPI.Net.Core.IConstruct"/> by its item identifier.
        /// </summary>
        /// <param name="itemIdentifier">
        /// The item identifier of the construct to be returned.
        /// </param>
        /// <returns>
        /// A construct with the specified item identifier or <c>null</c> if no such construct exists in the topic map.
        /// </returns>
        IConstruct GetConstructByItemIdentifier(ILocator itemIdentifier);

        /// <summary>
        /// Returns an index instance for this topic map using the specified generic type.
        /// </summary>
        /// <typeparam name="T">
        /// The data type of the index for this topic map.
        /// </typeparam>
        /// <returns>
        /// The index instance for this topic map.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// If the implementation does not support indices or if the specified index is unsupported.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
                "CA1004:GenericMethodsShouldProvideTypeParameter",
                Justification =
                        "As discussed in http://social.msdn.microsoft.com/Forums/en-US/vstscode/thread/81f356fd-3821-47bd-ad30-c0a6b70aade5/ there is no better solution.")]
        T GetIndex<T>() where T : class, IIndex;

        /// <summary>
        /// Returns a topic by its subject identifier.
        /// </summary>
        /// <param name="subjectIdentifier">
        /// The subject identifier of the topic to be returned.
        /// </param>
        /// <returns>
        /// A topic with the specified subject identifier or <c>null</c> if no such topic exists in the topic map.
        /// </returns>
        ITopic GetTopicBySubjectIdentifier(ILocator subjectIdentifier);

        /// <summary>
        /// Returns a topic by its subject locator.
        /// </summary>
        /// <param name="subjectLocator">
        /// The subject locator of the topic to be returned.
        /// </param>
        /// <returns>
        /// A topic with the specified subject locator or <c>null</c> if no such topic exists in the topic map.
        /// </returns>
        ITopic GetTopicBySubjectLocator(ILocator subjectLocator);

        /// <summary>
        /// Merges the topic map <paramref name="other"/> into this topic map.
        /// All <see cref="T:TMAPI.Net.Core.ITopic"/>s and <see cref="T:TMAPI.Net.Core.IAssociation"/>s and all of 
        /// their contents in <paramref name="other"/> will be added to this topic map.
        /// All information items in <paramref name="other"/> will be merged into this topic map as defined by the 
        /// <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-merging">Topic Maps - Data Model (TMDM) merging rules</a>.
        /// </summary>
        /// <param name="other">
        /// The topic map to be merged with this topic map instance, must not be <c>null</c>.
        /// </param>
        /// <remarks>
        /// <para>
        /// The merge process will not modify <paramref name="other"/> in any way.
        /// </para>
        /// <para>
        /// If <tt>this.Equals(other)</tt> no changes are made to the topic map.
        /// </para>
        /// </remarks>
        /// <exception cref="ModelConstraintException">
        /// If <paramref name="other"/> is <c>null</c>.
        /// </exception>
        void MergeIn(ITopicMap other);

        #endregion
    }
}