using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements the <see cref="ITopic"/> interface.
	/// </summary>
	public class Topic : Construct, ITopic
	{
		#region readonly & static fields
		/// <summary>
		/// Represents the list of names for this topic.
		/// </summary>
		private readonly List<IName> names;

		private readonly ReadOnlyCollection<IName> namesReadOnly;

		/// <summary>
		/// Represents the list of <see cref="IOccurrence">occurrences</see> attached to this topic.
		/// </summary>
		private readonly List<IOccurrence> occurrences;

		private readonly ReadOnlyCollection<IOccurrence> occurrencesReadOnly;

		/// <summary>
		/// Represents the list of roles played by this topic.
		/// </summary>
		private readonly List<IRole> rolesPlayed;

		private readonly ReadOnlyCollection<IRole> rolesPlayedReadOnly;

		/// <summary>
		/// Represents the list of subject identifiers for this topic.
		/// </summary>
		private readonly List<ILocator> subjectIdentifiers;

		private readonly ReadOnlyCollection<ILocator> subjectIdentifiersReadOnly;

		/// <summary>
		/// Represents the list of subject locators for this topic.
		/// </summary>
		private readonly List<ILocator> subjectLocators;

		private readonly ReadOnlyCollection<ILocator> subjectLocatorsReadOnly;

		/// <summary>
		/// Represents the list of types this topic is an instance of.
		/// </summary>
		private readonly List<ITopic> types;

		private readonly ReadOnlyCollection<ITopic> typesReadOnly;
		#endregion

		#region fields
		private IReifiable reified;
		#endregion

		#region constructor logic
		/// <summary>
		/// Initializes a new instance of the <see cref="Topic"/> class.
		/// </summary>
		/// <param name="topicMap">The <see cref="ITopicMap"/> containing this instance.</param>
		public Topic(ITopicMap topicMap)
			: base(topicMap, topicMap)
		{
			names = new List<IName>();
			namesReadOnly = names.AsReadOnly();

			occurrences = new List<IOccurrence>();
			occurrencesReadOnly = occurrences.AsReadOnly();

			subjectIdentifiers = new List<ILocator>();
			subjectIdentifiersReadOnly = subjectIdentifiers.AsReadOnly();

			subjectLocators = new List<ILocator>();
			subjectLocatorsReadOnly = subjectLocators.AsReadOnly();

			rolesPlayed = new List<IRole>();
			rolesPlayedReadOnly = rolesPlayed.AsReadOnly();

			types = new List<ITopic>();
			typesReadOnly = types.AsReadOnly();
		}
		#endregion

		#region ITopic properties
		/// <summary>
		///     Gets the names of this topic.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable set of <see cref="T:TMAPI.Net.Core.IName"/>s belonging to this topic.
		/// </returns>
		public ReadOnlyCollection<IName> Names
		{
			get
			{
				return namesReadOnly;
			}
		}

		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s of this topic.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable set of <see cref="T:TMAPI.Net.Core.IOccurrence"/>s belonging to this topic.
		/// </returns>
		public ReadOnlyCollection<IOccurrence> Occurrences
		{
			get
			{
				return occurrencesReadOnly;
			}
		}

		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.ITopicMap"/> to which this topic belongs.
		/// </summary>
		/// <returns>
		///     The <see cref="T:TMAPI.Net.Core.ITopicMap"/> to which this topic belongs.
		/// </returns>
		public new ITopicMap Parent
		{
			get
			{
				return TopicMap;
			}
		}

		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.IConstruct"/> which is reified by this topic.
		/// </summary>
		/// <returns>
		///     The <see cref="T:TMAPI.Net.Core.IReifiable"/> that is reified by this topic or 
		///     <c>null</c> if this topic does not reify a statement.
		/// </returns>
		public IReifiable Reified
		{
			get
			{
				return reified;
			}
			internal set
			{
				reified = value;
			}
		}

		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.IRole"/>s played by this topic.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable set of <see cref="T:TMAPI.Net.Core.IRole"/>s played by this topic.
		/// </returns>
		public ReadOnlyCollection<IRole> RolesPlayed
		{
			get
			{
				return rolesPlayedReadOnly;
			}
		}

		/// <summary>
		///     Gets the subject identifiers assigned to this topic.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable set of <see cref="T:TMAPI.Net.Core.ILocator"/>s representing the subject identifiers.
		/// </returns>
		public ReadOnlyCollection<ILocator> SubjectIdentifiers
		{
			get
			{
				return subjectIdentifiersReadOnly;
			}
		}

		/// <summary>
		///     Gets the subject locators assigned to this topic.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable set of <see cref="T:TMAPI.Net.Core.ILocator"/>s representing the subject locators.
		/// </returns>
		public ReadOnlyCollection<ILocator> SubjectLocators
		{
			get
			{
				return subjectLocatorsReadOnly;
			}
		}

		/// <summary>
		///     Gets the types of which this topic is an instance of.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable set of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
		/// </returns>
		/// <remarks>
		///     This method may return only those types which where added by <see cref="TMAPI.Net.Core.ITopic.AddType"/> 
		///     and may ignore <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-types">type-instance</a> 
		///     relationships which are modeled as association.
		/// </remarks>
		public ReadOnlyCollection<ITopic> Types
		{
			get
			{
				return typesReadOnly;
			}
		}
		#endregion

		#region ITopic methods
		/// <summary>
		/// Adds a subject identifier to this topic.
		/// If adding the specified subject identifier would make this topic represent the same subject
		/// as another topic and the feature "<c>automerge</c>" (http://tmapi.org/features/automerge) is disabled,
		/// an <see cref="IdentityConstraintException"/> is thrown.
		/// </summary>
		/// <param name="subjectIdentifier">The subject identifier to be added; must not be <c>null</c>.</param>
		/// <exception cref="ModelConstraintException">
		/// If the subject identifier is <c>null</c>.
		/// </exception>
		/// <exception cref="IdentityConstraintException">
		/// If adding <paramref name="subjectIdentifier"/> would make this topic represent the same subject
		/// as another topic and the feature "<c>automerge</c>" (http://tmapi.org/features/automerge) is disabled.
		/// </exception>
		public void AddSubjectIdentifier(ILocator subjectIdentifier)
		{
			if (subjectIdentifier == null)
			{
				throw new ModelConstraintException(
					"A subject identifier MUST NOT be null.",
					new ArgumentNullException("subjectIdentifier"));
			}

			subjectIdentifiers.Add(subjectIdentifier);

			// TODO add support for automerge and IdentityConstraintException
		}

		/// <summary>
		/// Adds a subject locator to this topic.
		/// If adding the specified subject locator would make this topic represent the same subject
		/// as another topic and the feature "<c>automerge</c>" (http://tmapi.org/features/automerge) is disabled,
		/// an <see cref="IdentityConstraintException"/> is thrown.
		/// </summary>
		/// <param name="subjectLocator">The subject locator to be added; must not be <c>null</c>.</param>
		/// <exception cref="ModelConstraintException">
		/// If the subject locator is <c>null</c>.
		/// </exception>
		/// <exception cref="IdentityConstraintException">
		/// If adding <paramref name="subjectLocator"/> would make this topic represent the same subject
		/// as another topic and the feature "<c>automerge</c>" (http://tmapi.org/features/automerge) is disabled.
		/// </exception>
		public void AddSubjectLocator(ILocator subjectLocator)
		{
			if (subjectLocator == null)
			{
				throw new ModelConstraintException(
					"A subject locator MUST NOT be null.",
					new ArgumentNullException("subjectLocator"));
			}

			subjectLocators.Add(subjectLocator);

			// TODO add support for automerge and IdentityConstraintException
		}

		/// <summary>
		///     Adds a type to this topic.
		///     Implementations may or may not create an association for types added by this method. 
		///     In any case, every type which was added by this method must be returned by the 
		///     <see cref="P:TMAPI.Net.Core.ITopic.Types"/> property.
		/// </summary>
		/// <param name="type">
		///     The type of which this topic should become an instance of; must not be <c>null</c>.
		/// </param>
		/// <exception cref="ModelConstraintException">
		///     If the <paramref name="type"/> is <c>null</c>.
		/// </exception>
		public void AddType(ITopic type)
		{
			if (type == null)
			{
				throw new ModelConstraintException(
					"A type MUST NOT be null.",
					new ArgumentNullException("type"));
			}

			types.Add(type);
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IName"/> for this topic with the specified <paramref name="type"/>, 
		///     <paramref name="value"/>, and <paramref name="themes"/>.
		/// </summary>
		/// <param name="type">
		///     The name type; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="value">
		///     The string value of the name; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="themes">
		///     An optional array of themes, MUST NOT be <c>null</c>.
		///     If the array's length is <c>0</c>, the name will be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IName"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If either the <paramref name="type"/>, the <paramref name="value"/>, 
		///     or the <paramref name="themes"/> is <c>null</c>.
		/// </exception>
		public IName CreateName(ITopic type, string value, params ITopic[] themes)
		{
			if (themes == null)
			{
				throw new ModelConstraintException(
					"The scope themes for a name instance MUST NOT be null.",
					new ArgumentNullException("themes"));
			}

			return CreateName(type, value, new List<ITopic>(themes));
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IName"/> for this topic with the specified <paramref name="type"/>, 
		///     <paramref name="value"/>, and <paramref name="themes"/>.
		/// </summary>
		/// <param name="type">
		///     The name type; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="value">
		///     The string value of the name; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="themes">
		///     A collection of themes or <c>null</c> if the name should be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IName"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If either the <paramref name="type"/> or the <paramref name="value"/> is <c>null</c>.
		/// </exception>
		public IName CreateName(ITopic type, string value, IList<ITopic> themes)
		{
			if (type == null)
			{
				throw new ModelConstraintException(
					"The type of a Name MUST NOT be null.",
					new ArgumentNullException("type"));
			}

			Name name = new Name(this, type);
			name.Value = value;
			name.OnRemove += Name_OnRemove;

			if (themes != null)
			{
				name.AddThemes(themes);
			}

			names.Add(name);

			return name;
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IName"/> for this topic with the specified <paramref name="value"/> 
		///     and <paramref name="themes"/>.
		///     The created <see cref="T:TMAPI.Net.Core.IName"/> will have the default name type 
		///     (a <see cref="T:TMAPI.Net.Core.ITopic"/> with the subject identifier 
		///     <a href="http://psi.topicmaps.org/iso13250/model/topic-name">http://psi.topicmaps.org/iso13250/model/topic-name</a>).
		/// </summary>
		/// <param name="value">
		///     The string value of the name; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="themes">
		///     An optional array of themes, MUST NOT be <c>null</c>.
		///     If the array's length is <c>0</c>, the name will be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IName"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If either the <paramref name="value"/> or the <paramref name="themes"/> is <c>null</c>.
		/// </exception>
		public IName CreateName(string value, params ITopic[] themes)
		{
			if (themes == null)
			{
				throw new ModelConstraintException(
					"The scopes can be empty but MUST NOT be null.",
					new ArgumentNullException("themes"));
			}

			return CreateName(value, new List<ITopic>(themes));
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IName"/> for this topic with the specified <paramref name="value"/> 
		///     and <paramref name="themes"/>.
		///     The created <see cref="T:TMAPI.Net.Core.IName"/> will have the default name type 
		///     (a <see cref="T:TMAPI.Net.Core.ITopic"/> with the subject identifier 
		///     <a href="http://psi.topicmaps.org/iso13250/model/topic-name">http://psi.topicmaps.org/iso13250/model/topic-name</a>).
		/// </summary>
		/// <param name="value">
		///     The string value of the name; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="themes">
		///     A collection of themes or <c>null</c> if the name should be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IName"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If the <paramref name="value"/> is <c>null</c>.
		/// </exception>
		public IName CreateName(string value, IList<ITopic> themes)
		{
			if (value == null)
			{
				throw new ModelConstraintException(
					"The value for a name MUST NOT be null.",
					new ArgumentNullException("value"));
			}

			return
				CreateName(
					Parent.CreateTopicBySubjectIdentifier(Parent.CreateLocator("http://psi.topicmaps.org/iso13250/model/topic-name")),
					value,
					themes);
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IOccurrence"/> for this topic with the specified <paramref name="type"/>, 
		///     string <paramref name="value"/>, and <paramref name="themes"/>.
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/> will have the datatype 
		///     <a href="http://www.w3.org/TR/xmlschema-2/#string">xsd:string</a>.
		/// </summary>
		/// <param name="type">
		///     The occurrence type; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="value">
		///     The string value of the occurrence.
		/// </param>
		/// <param name="themes">
		///     An optional array of themes, MUST NOT be <c>null</c>.
		///     If the array's length is <c>0</c>, the occurrence will be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If either the <paramref name="type"/>, the <paramref name="value"/>, 
		///     or the <paramref name="themes"/> is <c>null</c>.
		/// </exception>
		public IOccurrence CreateOccurrence(ITopic type, string value, params ITopic[] themes)
		{
			if (themes == null)
			{
				throw new ModelConstraintException(
					"The scopes can be empty but MUST NOT be null.",
					new ArgumentNullException("themes"));
			}

			return CreateOccurrence(type, value, new List<ITopic>(themes));
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IOccurrence"/> for this topic with the specified <paramref name="type"/>, 
		///     string <paramref name="value"/>, and <paramref name="themes"/>.
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/> will have the datatype 
		///     <a href="http://www.w3.org/TR/xmlschema-2/#string"><c>xsd:string</c></a>.
		/// </summary>
		/// <param name="type">
		///     The occurrence type; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="value">
		///     The string value of the occurrence.
		/// </param>
		/// <param name="themes">
		///     A collection of themes or <c>null</c> if the occurrence should be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If either the <paramref name="type"/> or the <paramref name="value"/> is <c>null</c>.
		/// </exception>
		public IOccurrence CreateOccurrence(ITopic type, string value, IList<ITopic> themes)
		{
			if (value == null)
			{
				throw new ModelConstraintException(
					"An occurrence value MUST NOT be null.",
					new ArgumentNullException("value"));
			}

			if (themes == null)
			{
				throw new ModelConstraintException(
					"The themes can be empty but MUST NOT be null.",
					new ArgumentNullException("themes"));
			}

			Occurrence occurrence = CreateOccurrence(type, themes);
			occurrence.Value = value;

			return occurrence;
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IOccurrence"/> for this topic with the specified <paramref name="type"/>, 
		///     IRI <paramref name="value"/>, and <paramref name="themes"/>.
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/> will have the datatype 
		///     <a href="http://www.w3.org/TR/xmlschema-2/#anyURI">xsd:anyURI</a>.
		/// </summary>
		/// <param name="type">
		///     The occurrence type; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="value">
		///     A <see cref="T:TMAPI.Net.Core.ILocator"/> which represents an IRI.
		/// </param>
		/// <param name="themes">
		///     An optional array of themes, MUST NOT be <c>null</c>.
		///     If the array's length is <c>0</c>, the occurrence will be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If either the <paramref name="type"/>, the <paramref name="value"/>, 
		///     or the <paramref name="themes"/> is <c>null</c>.
		/// </exception>
		public IOccurrence CreateOccurrence(ITopic type, ILocator value, params ITopic[] themes)
		{
			if (themes == null)
			{
				throw new ModelConstraintException(
					"The scopes can be empty but MUST NOT be null.",
					new ArgumentNullException("themes"));
			}

			return CreateOccurrence(type, value, new List<ITopic>(themes));
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IOccurrence"/> for this topic with the specified <paramref name="type"/>, 
		///     IRI <paramref name="value"/>, and <paramref name="themes"/>.
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/> will have the <c>datatype</c> 
		///     <a href="http://www.w3.org/TR/xmlschema-2/#anyURI"><c>xsd:anyURI</c></a>.
		/// </summary>
		/// <param name="type">
		///     The occurrence type; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="value">
		///     A <see cref="T:TMAPI.Net.Core.ILocator"/> which represents an IRI.
		/// </param>
		/// <param name="themes">
		///     A collection of themes or <c>null</c> if the occurrence should be in the unconstrained themes.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If either the <paramref name="type"/>, the <paramref name="value"/>, 
		///     or the <paramref name="themes"/> is <c>null</c>.
		/// </exception>
		public IOccurrence CreateOccurrence(ITopic type, ILocator value, IList<ITopic> themes)
		{
			if (value == null)
			{
				throw new ModelConstraintException(
					"An occurrence locator value MUST NOT be null.",
					new ArgumentNullException("value"));
			}

			if (themes == null)
			{
				throw new ModelConstraintException(
					"The themes can be empty but MUST NOT be null.",
					new ArgumentNullException("themes"));
			}

			Occurrence occurrence = CreateOccurrence(type, themes);
			occurrence.LocatorValue = value;

			return occurrence;
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IOccurrence"/> for this topic with the specified <paramref name="type"/>, 
		///     string <paramref name="value"/>, <paramref name="datatype"/> and <paramref name="themes"/>.
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/> will have the datatype 
		///     specified by <paramref name="datatype"/>.
		/// </summary>
		/// <param name="type">
		///     The occurrence type; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="value">
		///     The string value of the occurrence.
		/// </param>
		/// <param name="datatype">
		///     A <see cref="T:TMAPI.Net.Core.ILocator"/> indicating the datatype of the <paramref name="value"/>.
		/// </param>
		/// <param name="themes">
		///     An optional array of themes, MUST NOT be <c>null</c>.
		///     If the array's length is <c>0</c>, the occurrence will be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If either the <paramref name="type"/>, the <paramref name="value"/>, 
		///     the <paramref name="datatype"/> or the <paramref name="themes"/> is <c>null</c>.
		/// </exception>
		public IOccurrence CreateOccurrence(ITopic type, string value, ILocator datatype, params ITopic[] themes)
		{
			if (themes == null)
			{
				throw new ModelConstraintException(
					"The themes can be empty but MUST NOT be null.",
					new ArgumentNullException("themes"));
			}

			return CreateOccurrence(type, value, datatype, new List<ITopic>(themes));
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IOccurrence"/> for this topic with the specified <paramref name="type"/>, 
		///     string <paramref name="value"/>, <paramref name="datatype"/> and <paramref name="themes"/>.
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/> will have the datatype 
		///     specified by <paramref name="datatype"/>.
		/// </summary>
		/// <param name="type">
		///     The occurrence type; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="value">
		///     The string value of the occurrence.
		/// </param>
		/// <param name="datatype">
		///     A <see cref="T:TMAPI.Net.Core.ILocator"/> indicating the datatype of the <paramref name="value"/>.
		/// </param>
		/// <param name="themes">
		///     A collection of themes or <c>null</c> if the occurrence should be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If either the <paramref name="type"/>, the <paramref name="value"/>, 
		///     the <paramref name="datatype"/> or the <paramref name="themes"/> is <c>null</c>.
		/// </exception>
		public IOccurrence CreateOccurrence(ITopic type, string value, ILocator datatype, IList<ITopic> themes)
		{
			if (themes == null)
			{
				throw new ModelConstraintException(
					"The themes can be empty but MUST NOT be null.",
					new ArgumentNullException("themes"));
			}

			Occurrence occurrence = CreateOccurrence(type, themes);
			occurrence.SetValue(value, datatype);

			return occurrence;
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.IName">names</see> of this topic where the name type is <paramref name="type"/>.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="type">
		///     The type of the <see cref="T:TMAPI.Net.Core.IName">names</see> to be returned; must not be <c>null</c>.
		/// </param>
		/// <returns>
		///     An unmodifiable set of <see cref="T:TMAPI.Net.Core.IName">names</see> with the specified <paramref name="type"/>.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		///     If the <paramref name="type"/> is <c>null</c>.
		/// </exception>
		public ReadOnlyCollection<IName> GetNamesByTopicType(ITopic type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}

			List<IName> foundNames = new List<IName>();

			foreach (IName name in names)
			{
				if (name.Type == type)
				{
					foundNames.Add(name);
				}
			}

			return foundNames.AsReadOnly();
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.IOccurrence">occurrences</see> of this topic where the occurrence type is <paramref name="type"/>.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="type">
		///     The type of the <see cref="T:TMAPI.Net.Core.IOccurrence">occurrences</see> to be returned; must not be <c>null</c>.
		/// </param>
		/// <returns>
		///     An unmodifiable set of <see cref="T:TMAPI.Net.Core.IOccurrence">occurrences</see> with the specified <paramref name="type"/>.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		///     If the <paramref name="type"/> is <c>null</c>.
		/// </exception>
		public ReadOnlyCollection<IOccurrence> GetOccurrencesByTopicType(ITopic type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}

			List<IOccurrence> foundOccurrences = new List<IOccurrence>();

			foreach (IOccurrence occurrence in occurrences)
			{
				if (occurrence.Type == type)
				{
					foundOccurrences.Add(occurrence);
				}
			}

			return foundOccurrences.AsReadOnly();
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.IRole">roles</see> played by this topic where the role type is <paramref name="roleType"/>.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="roleType">
		///     The type of the <see cref="T:TMAPI.Net.Core.IRole">roles</see> to be returned; must not be <c>null</c>.
		/// </param>
		/// <returns>
		///     An unmodifiable set of <see cref="T:TMAPI.Net.Core.IRole">roles</see> with the specified <paramref name="roleType"/>.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		///     If the <paramref name="roleType"/> is <c>null</c>.
		/// </exception>
		public ReadOnlyCollection<IRole> GetRolesPlayedByTopicType(ITopic roleType)
		{
			if (roleType == null)
			{
				throw new ArgumentNullException("roleType");
			}

			List<IRole> foundRolesPlayed = new List<IRole>();

			foreach (IRole role in rolesPlayed)
			{
				if (role.Type == roleType)
				{
					foundRolesPlayed.Add(role);
				}
			}

			return foundRolesPlayed.AsReadOnly();
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.IRole">roles</see> played by this topic where the role type is <paramref name="roleType"/> 
		///     and the <see cref="T:TMAPI.Net.Core.IAssociation"/> type is <paramref name="associationType"/>.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="roleType">
		///     The type of the <see cref="T:TMAPI.Net.Core.IRole">roles</see> to be returned; must not be <c>null</c>.
		/// </param>
		/// <param name="associationType">
		///     The type of the <see cref="T:TMAPI.Net.Core.IAssociation"/> from which the returned roles must be part of; 
		///     must not be <c>null</c>.
		/// </param>
		/// <returns>
		///     An unmodifiable set of <see cref="T:TMAPI.Net.Core.IRole">roles</see> with the specified <paramref name="roleType"/> 
		///     which are part of <see cref="T:TMAPI.Net.Core.IAssociation">associations</see> with the specified <paramref name="associationType"/>.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		///     If the <paramref name="roleType"/> or <paramref name="associationType"/> is <c>null</c>.
		/// </exception>
		public ReadOnlyCollection<IRole> GetRolesPlayedByTopicTypeAndAssociationType(ITopic roleType, ITopic associationType)
		{
			if (roleType == null)
			{
				throw new ArgumentNullException("roleType");
			}

			if (associationType == null)
			{
				throw new ArgumentNullException("associationType");
			}

			List<IRole> foundRolesPlayed = new List<IRole>();

			foreach (IRole role in rolesPlayed)
			{
				if (role.Type == roleType && role.Parent.Type == associationType)
				{
					foundRolesPlayed.Add(role);
				}
			}

			return foundRolesPlayed.AsReadOnly();
		}

		/// <summary>
		///     Merges another topic into this topic.
		///     Merging a topic into this topic causes this topic to gain all of the characteristics 
		///     of the other topic and to replace the other topic wherever it is used as type, theme, or reifier. 
		///     After this method completes, <paramref name="other"/> will have been removed from the <see cref="T:TMAPI.Net.Core.ITopicMap"/>.
		/// </summary>
		/// <param name="other">
		///     The topic to be merged into this topic.
		/// </param>
		/// <remarks>
		///     The other topic MUST belong to the same <see cref="T:TMAPI.Net.Core.ITopicMap"/> instance as this topic!
		/// </remarks>
		public void MergeIn(ITopic other)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		///     Removes a subject identifier from this topic.
		/// </summary>
		/// <param name="subjectIdentifier">
		///     The subject identifier t be removed.
		/// </param>
		public void RemoveSubjectIdentifier(ILocator subjectIdentifier)
		{
			if (subjectIdentifier == null)
			{
				return;
			}

			subjectIdentifiers.Remove(subjectIdentifier);
		}

		/// <summary>
		///     Removes a subject locator from this topic.
		/// </summary>
		/// <param name="subjectLocator">
		///     The subject locator to be removed.
		/// </param>
		public void RemoveSubjectLocator(ILocator subjectLocator)
		{
			if (subjectLocator == null)
			{
				return;
			}

			subjectLocators.Remove(subjectLocator);
		}

		/// <summary>
		///     Removes a type from this topic.
		/// </summary>
		/// <param name="type">
		///     The type to remove.
		/// </param>
		public void RemoveType(ITopic type)
		{
			if (type == null)
			{
				return;
			}

			types.Remove(type);
		}

		/// <summary>
		/// Deletes this topic from its parent container.
		/// </summary>
		/// <exception cref="TopicInUseException">
		/// If this instance is used as <see cref="Type"/>, <see cref="IRole.Player"/>,
		/// <see cref="IScoped.Scope">theme</see> or <see cref="IReifiable.Reifier"/>.
		/// </exception>
		public new void Remove()
		{
			if (Reified != null)
			{
				throw new TopicInUseException("Removing a topic used for reification");
			}

			// HACK: could be a bottleneck
			if (rolesPlayed.Count > 0)
			{
				throw new TopicInUseException("Removing a topic used as player is not allowed.");
			}

			foreach (ITopic topic in Parent.Topics)
			{
				if (topic.Types.Contains(this))
				{
					// TODO: ask tmapi mailing list if this is allowed or not
					throw new TopicInUseException(
						String.Format(
							"Removing a topic used as type for topic {0} is not allowed.",
							topic));
				}

				if (topic.GetOccurrencesByTopicType(this).Count > 0)
				{
					throw new TopicInUseException(
						String.Format(
							"Removing a topic used as type for occurrence of topic {0} is not allowed.",
							topic));
				}

				if (topic.GetNamesByTopicType(this).Count > 0)
				{
					throw new TopicInUseException(
						String.Format(
							"Removing a topic used as type for name of topic {0} is not allowed.",
							topic));
				}

				if (topic.GetRolesPlayedByTopicType(this).Count > 0)
				{
					throw new TopicInUseException(
						String.Format(
							"Removing a topic used as type for a role of topic {0} is not allowed.",
							topic));
				}

				foreach (IName name in topic.Names)
				{
					if (name.Scope.Contains(this))
					{
						throw new TopicInUseException(
							String.Format(
								"Removing a topic used as scope theme for the name {0} of topic {1} is not allowed.",
								name,
								topic));
					}

					foreach (IVariant variant in name.Variants)
					{
						if (variant.Scope.Contains(this))
						{
							throw new TopicInUseException(
								String.Format(
									"Removing a topic used as scope theme for a variant of name {0} of topic {1} is not allowed.",
									name,
									topic));
						}
					}
				}

				foreach (IOccurrence occurrence in topic.Occurrences)
				{
					if (occurrence.Scope.Contains(this))
					{
						throw new TopicInUseException(
							String.Format(
								"Removing a topic used as scope theme for the occurrence {0} of topic {1} is not allowed.",
								occurrence,
								topic));
					}
				}
			}

			foreach (IAssociation association in Parent.Associations)
			{
				if (association.Type == this)
				{
					throw new TopicInUseException(
						String.Format(
							"Removing a topic used as type for association {0} is not allowed.",
							association));
				}

				if (association.Scope.Contains(this))
				{
					throw new TopicInUseException(
						String.Format(
							"Removing a topic used as scope theme for the association {0} is not allowed.",
							association));
				}
			}

			base.Remove();
		}
		#endregion

		#region methods
		/// <summary>
		/// Adds a role to the <see cref="RolesPlayed"/> list if this instance is a player of the role.
		/// </summary>
		/// <param name="role">The role this instance plays.</param>
		internal void AddRolePlayed(IRole role)
		{
			if (role == null)
			{
				throw new ArgumentNullException("role");
			}

			if (role is Role)
			{
				((Role) role).OnRemove += RolePlayed_OnRemove;
				((Role) role).OnRolePlayerChanges += RolePlayed_OnRolePlayerChanges;
			}

			rolesPlayed.Add(role);
		}

		/// <summary>
		/// Creates a simple occurrence.
		/// </summary>
		/// <param name="type">The type of this occurrence.</param>
		/// <param name="themes">The themes to added to scope.</param>
		/// <returns></returns>
		private Occurrence CreateOccurrence(ITopic type, IList<ITopic> themes)
		{
			Occurrence occurrence = new Occurrence(this, type);
			occurrence.OnRemove += Occurrence_OnRemove;
			occurrences.Add(occurrence);

			if (themes != null)
			{
				occurrence.AddThemes(themes);
			}

			return occurrence;
		}

		private void Name_OnRemove(object sender, EventArgs e)
		{
			IName name = sender as IName;

			if (name != null)
			{
				names.Remove(name);
			}
		}

		/// <summary>
		/// Handles the OnRemove event of the Occurrence control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void Occurrence_OnRemove(object sender, EventArgs e)
		{
			if (sender is IOccurrence)
			{
				occurrences.Remove((IOccurrence) sender);
			}
		}

		/// <summary>
		/// Removes the role from the <see cref="RolesPlayed"/> list if this instance is a player of the role..
		/// </summary>
		/// <param name="role">The role.</param>
		private void RemoveRolePlayed(IRole role)
		{
			if (role == null)
			{
				return;
			}

			if (role is Role)
			{
				((Role) role).OnRolePlayerChanges -= RolePlayed_OnRolePlayerChanges;
				((Role) role).OnRemove -= RolePlayed_OnRemove;
			}

			rolesPlayed.Remove(role);
		}

		/// <summary>
		/// Handles the <see cref="Construct.OnRemove"/> event of the RolePlayed control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void RolePlayed_OnRemove(object sender, EventArgs e)
		{
			if (sender != null && sender is IRole)
			{
				RemoveRolePlayed((IRole) sender);
			}
		}

		/// <summary>
		/// Handles the <see cref="Role.OnRolePlayerChanges"/> event of the RolePlayed control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RolePlayerChangedEventArgs"/> instance containing the event data.</param>
		private void RolePlayed_OnRolePlayerChanges(object sender, RolePlayerChangedEventArgs e)
		{
			if (sender != null && sender is IRole &&
			    e != null && e.OldPlayer == this)
			{
				RemoveRolePlayed((IRole) sender);
			}
		}
		#endregion
	}
}