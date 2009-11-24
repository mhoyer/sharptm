// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITopic.cs">
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
//   <a href="http://www.isotopicmaps.org/sam/sam-model/#d0e739">topic item</a>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents a 
    /// <a href="http://www.isotopicmaps.org/sam/sam-model/#d0e739">topic item</a>.
    /// </summary>
    public interface ITopic : IConstruct
    {
        #region Properties

        /// <summary>
        /// Gets the names of this topic.
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <returns>
        /// An unmodifiable set of <see cref="T:TMAPI.Net.Core.IName"/>s belonging to this topic.
        /// </returns>
        ReadOnlyCollection<IName> Names
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s of this topic.
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <returns>
        /// An unmodifiable set of <see cref="T:TMAPI.Net.Core.IOccurrence"/>s belonging to this topic.
        /// </returns>
        ReadOnlyCollection<IOccurrence> Occurrences
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="T:TMAPI.Net.Core.ITopicMap"/> to which this topic belongs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:TMAPI.Net.Core.ITopicMap"/> to which this topic belongs.
        /// </returns>
        new ITopicMap Parent
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="T:TMAPI.Net.Core.IConstruct"/> which is reified by this topic.
        /// </summary>
        /// <returns>
        /// The <see cref="T:TMAPI.Net.Core.IReifiable"/> that is reified by this topic or 
        /// <c>null</c> if this topic does not reify a statement.
        /// </returns>
        IReifiable Reified
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="T:TMAPI.Net.Core.IRole"/>s played by this topic.
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <returns>
        /// An unmodifiable set of <see cref="T:TMAPI.Net.Core.IRole"/>s played by this topic.
        /// </returns>
        ReadOnlyCollection<IRole> RolesPlayed
        {
            get;
        }

        /// <summary>
        /// Gets the subject identifiers assigned to this topic.
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <returns>
        /// An unmodifiable set of <see cref="T:TMAPI.Net.Core.ILocator"/>s representing the subject identifiers.
        /// </returns>
        ReadOnlyCollection<ILocator> SubjectIdentifiers
        {
            get;
        }

        /// <summary>
        /// Gets the subject locators assigned to this topic.
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <returns>
        /// An unmodifiable set of <see cref="T:TMAPI.Net.Core.ILocator"/>s representing the subject locators.
        /// </returns>
        ReadOnlyCollection<ILocator> SubjectLocators
        {
            get;
        }

        /// <summary>
        /// Gets the types of which this topic is an instance of.
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <returns>
        /// An unmodifiable set of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
        /// </returns>
        /// <remarks>
        /// This method may return only those types which where added by <see cref="TMAPI.Net.Core.ITopic.AddType"/> 
        /// and may ignore <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-types">type-instance</a> 
        /// relationships which are modeled as association.
        /// </remarks>
        ReadOnlyCollection<ITopic> Types
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds an item identifier to this topic.
        /// If adding the specified item identifier would make this topic represent the same subject 
        /// as another topic and the feature "<c>automerge</c>" (http://tmapi.org/features/automerge) is disabled, 
        /// an <see cref="IdentityConstraintException"/> is thrown.
        /// </summary>
        /// <param name="itemIdentifier">
        /// The item identifier to be added; must not be <c>null</c>.
        /// </param>
        /// <exception cref="ModelConstraintException">
        /// If the <paramref name="itemIdentifier"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="IdentityConstraintException">
        /// If adding <paramref name="itemIdentifier"/> would make this topic represent the same subject 
        /// as another topic and the feature "<c>automerge</c>" (http://tmapi.org/features/automerge) is disabled.
        /// </exception>
        new void AddItemIdentifier(ILocator itemIdentifier);

        /// <summary>
        /// Adds a subject identifier to this topic.
        /// If adding the specified subject identifier would make this topic represent the same subject 
        /// as another topic and the feature "<c>automerge</c>" (http://tmapi.org/features/automerge) is disabled, 
        /// an <see cref="IdentityConstraintException"/> is thrown.
        /// </summary>
        /// <param name="subjectIdentifier">
        /// The subject identifier to be added; must not be <c>null</c>.
        /// </param>
        /// <exception cref="ModelConstraintException">
        /// If the <paramref name="subjectIdentifier"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="IdentityConstraintException">
        /// If adding <paramref name="subjectIdentifier"/> would make this topic represent the same subject 
        /// as another topic and the feature "<c>automerge</c>" (http://tmapi.org/features/automerge) is disabled.
        /// </exception>
        void AddSubjectIdentifier(ILocator subjectIdentifier);

        /// <summary>
        /// Adds a subject locator to this topic.
        /// If adding the specified subject locator would make this topic represent the same subject 
        /// as another topic and the feature "<c>automerge</c>" (http://tmapi.org/features/automerge) is disabled, 
        /// an <see cref="IdentityConstraintException"/> is thrown.
        /// </summary>
        /// <param name="subjectLocator">
        /// The subject locator to be added; must not be <c>null</c>.
        /// </param>
        /// <exception cref="ModelConstraintException">
        /// If the <paramref name="subjectLocator"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="IdentityConstraintException">
        /// If adding <paramref name="subjectLocator"/> would make this topic represent the same subject 
        /// as another topic and the feature "<c>automerge</c>" (http://tmapi.org/features/automerge) is disabled.
        /// </exception>
        void AddSubjectLocator(ILocator subjectLocator);

        /// <summary>
        /// Adds a type to this topic.
        /// Implementations may or may not create an association for types added by this method. 
        /// In any case, every type which was added by this method must be returned by the 
        /// <see cref="P:TMAPI.Net.Core.ITopic.Types"/>property.
        /// </summary>
        /// <param name="type">
        /// The type of which this topic should become an instance of; must not be <c>null</c>.
        /// </param>
        /// <exception cref="ModelConstraintException">
        /// If the <paramref name="type"/> is <c>null</c>.
        /// </exception>
        void AddType(ITopic type);

        /// <summary>
        /// Creates a <see cref="T:TMAPI.Net.Core.IName"/> for this topic with the specified <paramref name="type"/>, 
        /// <paramref name="value"/>, and <paramref name="scope"/>.
        /// </summary>
        /// <param name="type">
        /// The name type; MUST NOT be <c>null</c>.
        /// </param>
        /// <param name="value">
        /// The string value of the name; MUST NOT be <c>null</c>.
        /// </param>
        /// <param name="scope">
        /// An optional array of themes, MUST NOT be <c>null</c>.
        /// If the array's length is <c>0</c>, the name will be in the unconstrained scope.
        /// </param>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.IName"/>.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If either the <paramref name="type"/>, the <paramref name="value"/>, or the <paramref name="scope"/> is <c>null</c> or 
        /// if either the <paramref name="type"/> or at least one theme of <paramref name="scope"/> belongs to another topic map.
        /// </exception>
        IName CreateName(ITopic type, string value, params ITopic[] scope);

        /// <summary>
        /// Creates a <see cref="T:TMAPI.Net.Core.IName"/> for this topic with the specified <paramref name="type"/>, 
        /// <paramref name="value"/>, and <paramref name="scope"/>.
        /// </summary>
        /// <param name="type">
        /// The name type; MUST NOT be <c>null</c>.
        /// </param>
        /// <param name="value">
        /// The string value of the name; MUST NOT be <c>null</c>.
        /// </param>
        /// <param name="scope">
        /// A collection of themes. The collection may be empty if the name should be in the unconstrained scope.
        /// </param>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.IName"/>.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If either the <paramref name="type"/>, the <paramref name="value"/> or the <paramref name="scope"/> is <c>null</c> or 
        /// if either the <paramref name="type"/> or at least one theme of <paramref name="scope"/> belongs to another topic map.
        /// </exception>
        IName CreateName(ITopic type, string value, IList<ITopic> scope);

        /// <summary>
        /// Creates a <see cref="T:TMAPI.Net.Core.IName"/> for this topic with the specified <paramref name="value"/> 
        /// and <paramref name="scope"/>.
        /// The created <see cref="T:TMAPI.Net.Core.IName"/> will have the default name type 
        /// (a <see cref="T:TMAPI.Net.Core.ITopic"/> with the subject identifier 
        /// <a href="http://psi.topicmaps.org/iso13250/model/topic-name">http://psi.topicmaps.org/iso13250/model/topic-name</a>).
        /// </summary>
        /// <param name="value">
        /// The string value of the name; MUST NOT be <c>null</c>.
        /// </param>
        /// <param name="scope">
        /// An optional array of themes, MUST NOT be <c>null</c>.
        /// If the array's length is <c>0</c>, the name will be in the unconstrained scope.
        /// </param>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.IName"/>.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If either the <paramref name="value"/> or the <paramref name="scope"/> is <c>null</c>.
        /// </exception>
        IName CreateName(string value, params ITopic[] scope);

        /// <summary>
        /// Creates a <see cref="T:TMAPI.Net.Core.IName"/> for this topic with the specified <paramref name="value"/> 
        /// and <paramref name="scope"/>.
        /// The created <see cref="T:TMAPI.Net.Core.IName"/> will have the default name type 
        /// (a <see cref="T:TMAPI.Net.Core.ITopic"/> with the subject identifier 
        /// <a href="http://psi.topicmaps.org/iso13250/model/topic-name">http://psi.topicmaps.org/iso13250/model/topic-name</a>).
        /// </summary>
        /// <param name="value">
        /// The string value of the name; MUST NOT be <c>null</c>.
        /// </param>
        /// <param name="scope">
        /// A collection of themes. The collection may be empty if the name should be in the unconstrained scope.
        /// </param>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.IName"/>.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If the <paramref name="value"/> or the <paramref name="scope"/> is <c>null</c>.
        /// </exception>
        IName CreateName(string value, IList<ITopic> scope);

        /// <summary>
        /// Creates a <see cref="T:TMAPI.Net.Core.IOccurrence"/> for this topic with the specified <paramref name="type"/>, 
        /// string <paramref name="value"/>, and <paramref name="scope"/>.
        /// The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/> will have the datatype 
        /// <a href="http://www.w3.org/TR/xmlschema-2/#string">xsd:string</a>.
        /// </summary>
        /// <param name="type">
        /// The occurrence type; MUST NOT be <c>null</c>.
        /// </param>
        /// <param name="value">
        /// The string value of the occurrence.
        /// </param>
        /// <param name="scope">
        /// An optional array of themes, MUST NOT be <c>null</c>.
        ///     If the array's length is <c>0</c>, the occurrence will be in the unconstrained scope.
        /// </param>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/>.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If either the <paramref name="type"/>, the <paramref name="value"/>, or the <paramref name="scope"/> is <c>null</c> or
        /// if either the <paramref name="type"/> or at least one theme of <paramref name="scope"/> belongs to another topic map.
        /// </exception>
        IOccurrence CreateOccurrence(ITopic type, string value, params ITopic[] scope);

        /// <summary>
        /// Creates a <see cref="T:TMAPI.Net.Core.IOccurrence"/> for this topic with the specified <paramref name="type"/>, 
        /// string <paramref name="value"/>, and <paramref name="scope"/>.
        /// The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/> will have the datatype 
        /// <a href="http://www.w3.org/TR/xmlschema-2/#string"><c>xsd:string</c></a>.
        /// </summary>
        /// <param name="type">
        /// The occurrence type; MUST NOT be <c>null</c>.
        /// </param>
        /// <param name="value">
        /// The string value of the occurrence.
        /// </param>
        /// <param name="scope">
        /// A collection of themes. The collection may be empty if the occurrence should be in the unconstrained scope.
        /// </param>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/>.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If either the <paramref name="type"/>, the <paramref name="value"/>, or the <paramref name="scope"/> is <c>null</c> or 
        /// if either the <paramref name="type"/> or at least one theme of <paramref name="scope"/> belongs to another topic map.
        /// </exception>
        IOccurrence CreateOccurrence(ITopic type, string value, IList<ITopic> scope);

        /// <summary>
        /// Creates a <see cref="T:TMAPI.Net.Core.IOccurrence"/> for this topic with the specified <paramref name="type"/>, 
        /// IRI <paramref name="value"/>, and <paramref name="scope"/>.
        /// The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/> will have the datatype 
        /// <a href="http://www.w3.org/TR/xmlschema-2/#anyURI"><c>xsd:anyURI</c></a>.
        /// </summary>
        /// <param name="type">
        /// The occurrence type; MUST NOT be <c>null</c>.
        /// </param>
        /// <param name="value">
        /// A <see cref="T:TMAPI.Net.Core.ILocator"/> which represents an IRI.
        /// </param>
        /// <param name="scope">
        /// An optional array of themes, MUST NOT be <c>null</c>.
        /// If the array's length is <c>0</c>, the occurrence will be in the unconstrained scope.
        /// </param>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/>.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If either the <paramref name="type"/>, the <paramref name="value"/>, or the <paramref name="scope"/> is <c>null</c> or
        /// if either the <paramref name="type"/> or at least one theme of <paramref name="scope"/> belongs to another topic map.
        /// </exception>
        IOccurrence CreateOccurrence(ITopic type, ILocator value, params ITopic[] scope);

        /// <summary>
        /// Creates a <see cref="T:TMAPI.Net.Core.IOccurrence"/> for this topic with the specified <paramref name="type"/>, 
        /// IRI <paramref name="value"/>, and <paramref name="scope"/>.
        /// The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/> will have the datatype 
        /// <a href="http://www.w3.org/TR/xmlschema-2/#anyURI"><c>xsd:anyURI</c></a>.
        /// </summary>
        /// <param name="type">
        /// The occurrence type; MUST NOT be <c>null</c>.
        /// </param>
        /// <param name="value">
        /// A <see cref="T:TMAPI.Net.Core.ILocator"/> which represents an IRI.
        /// </param>
        /// <param name="scope">
        /// A collection of themes. The collection may be empty if the occurrence should be in the unconstrained scope.
        /// </param>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/>.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If either the <paramref name="type"/>, the <paramref name="value"/>, or the <paramref name="scope"/> is <c>null</c> or
        /// if either the <paramref name="type"/> or at least one theme of <paramref name="scope"/> belongs to another topic map.
        /// </exception>
        IOccurrence CreateOccurrence(ITopic type, ILocator value, IList<ITopic> scope);

        /// <summary>
        /// Creates a <see cref="T:TMAPI.Net.Core.IOccurrence"/> for this topic with the specified <paramref name="type"/>, 
        /// string <paramref name="value"/>, <paramref name="datatype"/> and <paramref name="scope"/>.
        /// The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/> will have the datatype 
        /// specified by <paramref name="datatype"/>.
        /// </summary>
        /// <param name="type">
        /// The occurrence type; MUST NOT be <c>null</c>.
        /// </param>
        /// <param name="value">
        /// The string value of the occurrence.
        /// </param>
        /// <param name="datatype">
        /// A <see cref="T:TMAPI.Net.Core.ILocator"/> indicating the datatype of the <paramref name="value"/>.
        /// </param>
        /// <param name="scope">
        /// An optional array of themes, MUST NOT be <c>null</c>.
        ///     If the array's length is <c>0</c>, the occurrence will be in the unconstrained scope.
        /// </param>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/>.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If either the <paramref name="type"/>, the <paramref name="value"/>, the <paramref name="datatype"/> or the <paramref name="scope"/> is <c>null</c> or
        /// if either the <paramref name="type"/> or at least one theme of <paramref name="scope"/> belongs to another topic map.
        /// </exception>
        IOccurrence CreateOccurrence(ITopic type, string value, ILocator datatype, params ITopic[] scope);

        /// <summary>
        /// Creates a <see cref="T:TMAPI.Net.Core.IOccurrence"/> for this topic with the specified <paramref name="type"/>, 
        /// string <paramref name="value"/>, <paramref name="datatype"/> and <paramref name="scope"/>.
        /// The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/> will have the datatype 
        /// specified by <paramref name="datatype"/>.
        /// </summary>
        /// <param name="type">
        /// The occurrence type; MUST NOT be <c>null</c>.
        /// </param>
        /// <param name="value">
        /// The string value of the occurrence.
        /// </param>
        /// <param name="datatype">
        /// A <see cref="T:TMAPI.Net.Core.ILocator"/> indicating the datatype of the <paramref name="value"/>.
        /// </param>
        /// <param name="scope">
        /// A collection of themes. The collection may be empty if the occurrence should be in the unconstrained scope.
        /// </param>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/>.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If either the <paramref name="type"/>, the <paramref name="value"/>, the <paramref name="datatype"/> or the <paramref name="scope"/> is <c>null</c> or
        /// if either the <paramref name="type"/> or at least one theme of <paramref name="scope"/> belongs to another topic map.
        /// </exception>
        IOccurrence CreateOccurrence(ITopic type, string value, ILocator datatype, IList<ITopic> scope);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IName">names</see> of this topic where the name type is <paramref name="type"/>.
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="type">
        /// The type of the <see cref="T:TMAPI.Net.Core.IName">names</see> to be returned; must not be <c>null</c>.
        /// </param>
        /// <returns>
        /// An unmodifiable set of <see cref="T:TMAPI.Net.Core.IName">names</see> with the specified <paramref name="type"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="type"/> is <c>null</c>.
        /// </exception>
        ReadOnlyCollection<IName> GetNamesByTopicType(ITopic type);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IOccurrence">occurrences</see> of this topic where the occurrence type is <paramref name="type"/>.
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="type">
        /// The type of the <see cref="T:TMAPI.Net.Core.IOccurrence">occurrences</see> to be returned; must not be <c>null</c>.
        /// </param>
        /// <returns>
        /// An unmodifiable set of <see cref="T:TMAPI.Net.Core.IOccurrence">occurrences</see> with the specified <paramref name="type"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="type"/> is <c>null</c>.
        /// </exception>
        ReadOnlyCollection<IOccurrence> GetOccurrencesByTopicType(ITopic type);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IRole">roles</see> played by this topic where the role type is <paramref name="roleType"/>.
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="roleType">
        /// The type of the <see cref="T:TMAPI.Net.Core.IRole">roles</see> to be returned; must not be <c>null</c>.
        /// </param>
        /// <returns>
        /// An unmodifiable set of <see cref="T:TMAPI.Net.Core.IRole">roles</see> with the specified <paramref name="roleType"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="roleType"/> is <c>null</c>.
        /// </exception>
        ReadOnlyCollection<IRole> GetRolesPlayedByTopicType(ITopic roleType);

        /// <summary>
        /// Returns the <see cref="T:TMAPI.Net.Core.IRole">roles</see> played by this topic where the role type is <paramref name="roleType"/> 
        /// and the <see cref="T:TMAPI.Net.Core.IAssociation"/> type is <paramref name="associationType"/>.
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <param name="roleType">
        /// The type of the <see cref="T:TMAPI.Net.Core.IRole">roles</see> to be returned; must not be <c>null</c>.
        /// </param>
        /// <param name="associationType">
        /// The type of the <see cref="T:TMAPI.Net.Core.IAssociation"/> from which the returned roles must be part of; 
        /// must not be <c>null</c>.
        /// </param>
        /// <returns>
        /// An unmodifiable set of <see cref="T:TMAPI.Net.Core.IRole">roles</see> with the specified <paramref name="roleType"/> 
        /// which are part of <see cref="T:TMAPI.Net.Core.IAssociation">associations</see> with the specified <paramref name="associationType"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="roleType"/> or <paramref name="associationType"/> is <c>null</c>.
        /// </exception>
        ReadOnlyCollection<IRole> GetRolesPlayedByTopicTypeAndAssociationType(ITopic roleType, ITopic associationType);

        /// <summary>
        /// Merges another topic into this topic.
        /// Merging a topic into this topic causes this topic to gain all of the characteristics 
        /// of the other topic and to replace the other topic wherever it is used as type, theme, or reifier. 
        /// After this method completes, <paramref name="other"/> will have been removed from the <see cref="T:TMAPI.Net.Core.ITopicMap"/>.
        /// </summary>
        /// <param name="other">
        /// The topic to be merged into this topic, must not be <c>null</c>.
        /// </param>
        /// <remarks>
        /// <para>
        /// The other topic MUST belong to the same <see cref="T:TMAPI.Net.Core.ITopicMap"/> instance as this topic!
        /// </para>
        /// <para>
        /// If <tt>this.Equals(other)</tt> no changes are made to the topic.
        /// </para>
        /// </remarks>
        /// <exception cref="ModelConstraintException">
        /// If <paramref name="other"/> is <c>null</c>.
        /// </exception>
        void MergeIn(ITopic other);

        /// <summary>
        /// Removes this topic from the containing <see cref="T:TMAPI.Net.Core.ITopicMap"/> instance.
        /// </summary>
        /// <exception cref="TopicInUseException">
        /// If the topic plays a <see cref="T:TMAPI.Net.Core.IRole"/>, is used as type of a 
        /// <see cref="T:TMAPI.Net.Core.ITyped"/>construct, or if it is used as theme for a 
        /// <see cref="T:TMAPI.Net.Core.IScoped"/>construct, or if it reifies a <see cref="T:TMAPI.Net.Core.IReifiable"/>.
        /// </exception>
        new void Remove();

        /// <summary>
        /// Removes a subject identifier from this topic.
        /// </summary>
        /// <param name="subjectIdentifier">
        ///  The subject identifier to be removed from this topic, if present (<c>null</c> is ignored).
        /// </param>
        void RemoveSubjectIdentifier(ILocator subjectIdentifier);

        /// <summary>
        /// Removes a subject locator from this topic.
        /// </summary>
        /// <param name="subjectLocator">
        /// The subject locator to be removed from this topic, if present (<c>null</c> is ignored).
        /// </param>
        void RemoveSubjectLocator(ILocator subjectLocator);

        /// <summary>
        /// Removes a type from this topic.
        /// </summary>
        /// <param name="type">
        /// The type to be removed from this topic, if present (<c>null</c> is ignored).
        /// </param>
        void RemoveType(ITopic type);

        #endregion
    }
}