// <copyright file="Topic.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
#if LOG4NET
using log4net;
#endif
using Pixelplastic.TopicMaps.SharpTM.Core.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Merging;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts.Entities;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements the <see cref="ITopic"/> interface.
	/// </summary>
	public class Topic : Construct, ITopic
	{
#if LOG4NET
		static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
#endif

		internal TopicData topicData;

		/// <summary>
		/// Initializes a new instance of the <see cref="Topic"/> class.
		/// </summary>
		/// <param name="entity">The underlying domain object.</param>
		/// <param name="topicMap">The <see cref="ITopicMap"/> containing this instance.</param>
		internal Topic(TopicEntity entity, ITopicMap topicMap)
			: base(entity, topicMap, topicMap)
		{
			if (entity == null) throw new ArgumentNullException("entity");

			topicData = new TopicData();
		}


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
			get { return topicData.Names; }
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
			get { return topicData.Occurrences; }
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
			internal set
			{
				TopicMap = value;
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
			get { return topicData.Reified; }
			internal set { topicData.Reified = value; }
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
			get { return topicData.RolesPlayed; }
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
			get { return topicData.SubjectIdentifiers; }
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
			get { return topicData.SubjectLocators; }
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
			get { return topicData.Types; }
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
#if LOG4NET
			log.InfoFormat("Adding subject identifier '{0}' to topic '{1}'.", subjectIdentifier, this);
#endif

			if (subjectIdentifier == null)
			{
				throw new ModelConstraintException(
					this,
					"A subject identifier MUST NOT be null.",
					new ArgumentNullException("subjectIdentifier"));
			}

			if (SubjectIdentifiers.Contains(subjectIdentifier))
			{
				return;
			}

			ITopic existingTopic = TopicMap.GetTopicBySubjectIdentifier(subjectIdentifier);

			if (existingTopic != null)
			{
				if (((TopicMap)TopicMap).TopicMapSystem.GetFeature(Features.AutomaticMerging))
				{
					existingTopic.MergeIn(this);
					return;
				}

				throw new IdentityConstraintException(
					this,
					existingTopic,
					subjectIdentifier,
					String.Format("Topic with subject identifier {0} already exists in topic map and [automerge] is not enabled.", subjectIdentifier));
			}
			
			IConstruct construct = TopicMap.GetConstructByItemIdentifier(subjectIdentifier);

			if (construct != null && construct is ITopic)
			{
				if (((TopicMap)TopicMap).TopicMapSystem.GetFeature(Features.AutomaticMerging))
				{
					// ((ITopic)construct).MergeIn(this);
					// ((ITopic)construct).AddSubjectIdentifier(subjectIdentifier);
					((ITopic)construct).MergeIn(this);
				}
				else
				{
					throw new IdentityConstraintException(
						this,
						construct,
						subjectIdentifier,
						String.Format("Topic with item identifier {0} already exists in topic map and [automerge] is not enabled.",
						              subjectIdentifier));
				}
			}

			topicData.SubjectIdentifiers.Add(subjectIdentifier);
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
#if LOG4NET
			log.InfoFormat("Adding subject locator '{0}' to topic '{1}'.", subjectLocator, this);
#endif

			if (subjectLocator == null)
			{
				throw new ModelConstraintException(
					this,
					"A subject locator MUST NOT be null.",
					new ArgumentNullException("subjectLocator"));
			}

			if (SubjectLocators.Contains(subjectLocator))
			{
				return;
			}

			ITopic existingTopic = TopicMap.GetTopicBySubjectLocator(subjectLocator);

			if (existingTopic != null)
			{
				if (((TopicMap)TopicMap).TopicMapSystem.GetFeature(Features.AutomaticMerging))
				{
					existingTopic.MergeIn(this);
					return;
				}

				throw new IdentityConstraintException(
					this,
					existingTopic,
					subjectLocator,
					String.Format("Topic with subject locator {0} already exists in topic map and [automerge] is not enabled.", subjectLocator));
			}
			
			topicData.SubjectLocators.Add(subjectLocator);
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
					this,
					"A type MUST NOT be null.",
					new ArgumentNullException("type"));
			}

			topicData.Types.Add(type);
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IName"/> for this topic with the specified <paramref name="type"/>, 
		///     <paramref name="value"/>, and <paramref name="scope"/>.
		/// </summary>
		/// <param name="type">
		///     The name type; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="value">
		///     The string value of the name; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="scope">
		///     An optional array of themes, MUST NOT be <c>null</c>.
		///     If the array's length is <c>0</c>, the name will be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IName"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If either the <paramref name="type"/>, the <paramref name="value"/>, 
		///     or the <paramref name="scope"/> is <c>null</c>.
		/// </exception>
		public IName CreateName(ITopic type, string value, params ITopic[] scope)
		{
			if (scope == null)
			{
				throw new ModelConstraintException(
					this,
					"The scope themes for a name instance MUST NOT be null.",
					new ArgumentNullException("scope"));
			}

			return CreateName(type, value, new List<ITopic>(scope));
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IName"/> for this topic with the specified <paramref name="type"/>, 
		///     <paramref name="value"/>, and <paramref name="scope"/>.
		/// </summary>
		/// <param name="type">
		///     The name type; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="value">
		///     The string value of the name; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="scope">
		///     A collection of themes or <c>null</c> if the name should be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IName"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If either the <paramref name="type"/> or the <paramref name="value"/> is <c>null</c>.
		/// </exception>
		public IName CreateName(ITopic type, string value, IList<ITopic> scope)
		{
			if (type == null)
			{
				throw new ModelConstraintException(
					this,
					"The type of a Name MUST NOT be null.",
					new ArgumentNullException("type"));
			}

			if (value == null)
			{
				throw new ModelConstraintException(
					this,
					"The value of a Name MUST NOT be null.",
					new ArgumentNullException("value"));
			}

			if (scope == null)
			{
				throw new ModelConstraintException(
					this,
					"The value of a Name MUST NOT be null.",
					new ArgumentNullException("scope"));
			}

			Name name = new Name(this, type);
			name.Value = value;
			name.AddThemes(scope);

			AddName(name);

			// HACK should be solved by delegates
			if (TopicMap is TopicMap)
			{
				((TopicMap) TopicMap).AddConstruct(name);
			}

			return name;
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IName"/> for this topic with the specified <paramref name="value"/> 
		///     and <paramref name="scope"/>.
		///     The created <see cref="T:TMAPI.Net.Core.IName"/> will have the default name type 
		///     (a <see cref="T:TMAPI.Net.Core.ITopic"/> with the subject identifier 
		///     <a href="http://psi.topicmaps.org/iso13250/model/topic-name">http://psi.topicmaps.org/iso13250/model/topic-name</a>).
		/// </summary>
		/// <param name="value">
		///     The string value of the name; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="scope">
		///     An optional array of themes, MUST NOT be <c>null</c>.
		///     If the array's length is <c>0</c>, the name will be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IName"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If either the <paramref name="value"/> or the <paramref name="scope"/> is <c>null</c>.
		/// </exception>
		public IName CreateName(string value, params ITopic[] scope)
		{
			if (scope == null)
			{
				throw new ModelConstraintException(
					this,
					"The scopes can be empty but MUST NOT be null.",
					new ArgumentNullException("scope"));
			}

			return CreateName(value, new List<ITopic>(scope));
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IName"/> for this topic with the specified <paramref name="value"/> 
		///     and <paramref name="scope"/>.
		///     The created <see cref="T:TMAPI.Net.Core.IName"/> will have the default name type 
		///     (a <see cref="T:TMAPI.Net.Core.ITopic"/> with the subject identifier 
		///     <a href="http://psi.topicmaps.org/iso13250/model/topic-name">http://psi.topicmaps.org/iso13250/model/topic-name</a>).
		/// </summary>
		/// <param name="value">
		///     The string value of the name; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="scope">
		///     A collection of themes or <c>null</c> if the name should be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IName"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If the <paramref name="value"/> is <c>null</c>.
		/// </exception>
		public IName CreateName(string value, IList<ITopic> scope)
		{
			if (value == null)
			{
				throw new ModelConstraintException(
					this,
					"The value for a name MUST NOT be null.",
					new ArgumentNullException("value"));
			}

			return
				CreateName(
					Parent.CreateTopicBySubjectIdentifier(Parent.CreateLocator("http://psi.topicmaps.org/iso13250/model/topic-name")),
					value,
					scope);
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IOccurrence"/> for this topic with the specified <paramref name="type"/>, 
		///     string <paramref name="value"/>, and <paramref name="scope"/>.
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/> will have the datatype 
		///     <a href="http://www.w3.org/TR/xmlschema-2/#string">xsd:string</a>.
		/// </summary>
		/// <param name="type">
		///     The occurrence type; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="value">
		///     The string value of the occurrence.
		/// </param>
		/// <param name="scope">
		///     An optional array of themes, MUST NOT be <c>null</c>.
		///     If the array's length is <c>0</c>, the occurrence will be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If either the <paramref name="type"/>, the <paramref name="value"/>, 
		///     or the <paramref name="scope"/> is <c>null</c>.
		/// </exception>
		public IOccurrence CreateOccurrence(ITopic type, string value, params ITopic[] scope)
		{
			if (scope == null)
			{
				throw new ModelConstraintException(
					this,
					"The scopes can be empty but MUST NOT be null.",
					new ArgumentNullException("scope"));
			}

			return CreateOccurrence(type, value, new List<ITopic>(scope));
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IOccurrence"/> for this topic with the specified <paramref name="type"/>, 
		///     string <paramref name="value"/>, and <paramref name="scope"/>.
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/> will have the datatype 
		///     <a href="http://www.w3.org/TR/xmlschema-2/#string"><c>xsd:string</c></a>.
		/// </summary>
		/// <param name="type">
		///     The occurrence type; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="value">
		///     The string value of the occurrence.
		/// </param>
		/// <param name="scope">
		///     A collection of themes or <c>null</c> if the occurrence should be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If either the <paramref name="type"/> or the <paramref name="value"/> is <c>null</c>.
		/// </exception>
		public IOccurrence CreateOccurrence(ITopic type, string value, IList<ITopic> scope)
		{
			if (value == null)
			{
				throw new ModelConstraintException(
					this,
					"An occurrence value MUST NOT be null.",
					new ArgumentNullException("value"));
			}

			if (scope == null)
			{
				throw new ModelConstraintException(
					this,
					"The themes can be empty but MUST NOT be null.",
					new ArgumentNullException("scope"));
			}

			Occurrence occurrence = CreateOccurrence(type, scope);
			occurrence.Value = value;

			// HACK should be solved by delegates
			if (TopicMap is TopicMap)
			{
				((TopicMap) TopicMap).AddConstruct(occurrence);
			}

			return occurrence;
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IOccurrence"/> for this topic with the specified <paramref name="type"/>, 
		///     IRI <paramref name="value"/>, and <paramref name="scope"/>.
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/> will have the datatype 
		///     <a href="http://www.w3.org/TR/xmlschema-2/#anyURI">xsd:anyURI</a>.
		/// </summary>
		/// <param name="type">
		///     The occurrence type; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="value">
		///     A <see cref="T:TMAPI.Net.Core.ILocator"/> which represents an IRI.
		/// </param>
		/// <param name="scope">
		///     An optional array of themes, MUST NOT be <c>null</c>.
		///     If the array's length is <c>0</c>, the occurrence will be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If either the <paramref name="type"/>, the <paramref name="value"/>, 
		///     or the <paramref name="scope"/> is <c>null</c>.
		/// </exception>
		public IOccurrence CreateOccurrence(ITopic type, ILocator value, params ITopic[] scope)
		{
			if (scope == null)
			{
				throw new ModelConstraintException(
					this,
					"The scopes can be empty but MUST NOT be null.",
					new ArgumentNullException("scope"));
			}

			return CreateOccurrence(type, value, new List<ITopic>(scope));
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IOccurrence"/> for this topic with the specified <paramref name="type"/>, 
		///     IRI <paramref name="value"/>, and <paramref name="scope"/>.
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/> will have the <c>datatype</c> 
		///     <a href="http://www.w3.org/TR/xmlschema-2/#anyURI"><c>xsd:anyURI</c></a>.
		/// </summary>
		/// <param name="type">
		///     The occurrence type; MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="value">
		///     A <see cref="T:TMAPI.Net.Core.ILocator"/> which represents an IRI.
		/// </param>
		/// <param name="scope">
		///     A collection of themes or <c>null</c> if the occurrence should be in the unconstrained themes.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If either the <paramref name="type"/>, the <paramref name="value"/>, 
		///     or the <paramref name="scope"/> is <c>null</c>.
		/// </exception>
		public IOccurrence CreateOccurrence(ITopic type, ILocator value, IList<ITopic> scope)
		{
			if (value == null)
			{
				throw new ModelConstraintException(
					this,
					"An occurrence locator value MUST NOT be null.",
					new ArgumentNullException("value"));
			}

			if (scope == null)
			{
				throw new ModelConstraintException(
					this,
					"The themes can be empty but MUST NOT be null.",
					new ArgumentNullException("scope"));
			}

			Occurrence occurrence = CreateOccurrence(type, scope);
			occurrence.LocatorValue = value;

			return occurrence;
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IOccurrence"/> for this topic with the specified <paramref name="type"/>, 
		///     string <paramref name="value"/>, <paramref name="datatype"/> and <paramref name="scope"/>.
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
		/// <param name="scope">
		///     An optional array of themes, MUST NOT be <c>null</c>.
		///     If the array's length is <c>0</c>, the occurrence will be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If either the <paramref name="type"/>, the <paramref name="value"/>, 
		///     the <paramref name="datatype"/> or the <paramref name="scope"/> is <c>null</c>.
		/// </exception>
		public IOccurrence CreateOccurrence(ITopic type, string value, ILocator datatype, params ITopic[] scope)
		{
			if (scope == null)
			{
				throw new ModelConstraintException(
					this,
					"The themes can be empty but MUST NOT be null.",
					new ArgumentNullException("scope"));
			}

			return CreateOccurrence(type, value, datatype, new List<ITopic>(scope));
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IOccurrence"/> for this topic with the specified <paramref name="type"/>, 
		///     string <paramref name="value"/>, <paramref name="datatype"/> and <paramref name="scope"/>.
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
		/// <param name="scope">
		///     A collection of themes or <c>null</c> if the occurrence should be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IOccurrence"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If either the <paramref name="type"/>, the <paramref name="value"/>, 
		///     the <paramref name="datatype"/> or the <paramref name="scope"/> is <c>null</c>.
		/// </exception>
		public IOccurrence CreateOccurrence(ITopic type, string value, ILocator datatype, IList<ITopic> scope)
		{
			if (scope == null)
			{
				throw new ModelConstraintException(
					this,
					"The themes can be empty but MUST NOT be null.",
					new ArgumentNullException("scope"));
			}

			if (datatype == null)
			{
				throw new ModelConstraintException(
					this,
					"The datatype of an occurrence MUST NOT be null.",
					new ArgumentNullException("datatype"));
			}

			Occurrence occurrence = CreateOccurrence(type, scope);
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

			foreach (IName name in Names)
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

			foreach (IOccurrence occurrence in Occurrences)
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

			foreach (IRole role in RolesPlayed)
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

			foreach (IRole role in RolesPlayed)
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
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}

			if (this == other)
			{
				return;
			}

			if (other.Parent != Parent)
			{
				throw new TMAPIException("The other topic MUST belong to the same Topic Map instance as this topic");
			}

			Merge.Topic(other).Into(this);
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

			topicData.SubjectIdentifiers.Remove(subjectIdentifier);
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

			topicData.SubjectLocators.Remove(subjectLocator);
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

			topicData.Types.Remove(type);
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
#if LOG4NET
			log.InfoFormat("Removing Topic '{0}'.", this);
#endif

			if (Reified != null)
			{
				throw new TopicInUseException(this, "Removing a topic used for reification");
			}

			// HACK: could be a bottleneck
			if (RolesPlayed.Count > 0)
			{
				throw new TopicInUseException(this, "Removing a topic used as player is not allowed.");
			}

			foreach (ITopic topic in Parent.Topics)
			{
				if (topic.Types.Contains(this))
				{
					// TODO: ask tmapi mailing list if this is allowed or not
					throw new TopicInUseException(
						this,
						String.Format(
							"Removing a topic used as type for topic {0} is not allowed.",
							topic));
				}

				if (topic.GetOccurrencesByTopicType(this).Count > 0)
				{
					throw new TopicInUseException(
						this,
						String.Format(
							"Removing a topic used as type for occurrence of topic {0} is not allowed.",
							topic));
				}

				if (topic.GetNamesByTopicType(this).Count > 0)
				{
					throw new TopicInUseException(
						this,
						String.Format(
							"Removing a topic used as type for name of topic {0} is not allowed.",
							topic));
				}

				if (topic.GetRolesPlayedByTopicType(this).Count > 0)
				{
					throw new TopicInUseException(
						this,
						String.Format(
							"Removing a topic used as type for a role of topic {0} is not allowed.",
							topic));
				}

				foreach (IName name in topic.Names)
				{
					if (name.Scope.Contains(this))
					{
						throw new TopicInUseException(
							this,
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
								this,
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
							this,
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
						this,
						String.Format(
							"Removing a topic used as type for association {0} is not allowed.",
							association));
				}

				if (association.Scope.Contains(this))
				{
					throw new TopicInUseException(
						this,
						String.Format(
							"Removing a topic used as scope theme for the association {0} is not allowed.",
							association));
				}
			}

			base.Remove();
		}
		#endregion

		/// <summary>
		/// Determines whether the specified topic is equal to this instance.
		/// </summary>
		/// <param name="other">The other topic.</param>
		/// <returns>
		/// 	<c>true</c> if the specified topic is equal; otherwise, <c>false</c>.
		/// </returns>
		public bool Equals(ITopic other)
		{
			if (this == other)
			{
				return true;
			}

			if (topicData.SubjectIdentifiers.Exists(
				locator =>
					{
						return other.SubjectIdentifiers.Contains(locator) ||
						       other.ItemIdentifiers.Contains(locator);
					}))
			{
				return true;
			}

			if (topicData.SubjectLocators.Exists(locator => other.SubjectLocators.Contains(locator)))
			{
				return true;
			}

			foreach (ILocator locator in ItemIdentifiers)
			{
				if (other.SubjectIdentifiers.Contains(locator))
				{
					return true;
				}

				if (other.ItemIdentifiers.Contains(locator))
				{
					return true;
				}
			}

			if (Reified != null)
			{
				return Reified.Equals(other.Reified);
			}

			return false;
		}

		/// <summary>
		/// Adds the name to the internal list of names for this topic.
		/// </summary>
		/// <remarks>
		/// Replaces <see cref="Name.Parent"/> to this <see cref="Topic"/> instance.
		/// </remarks>
		/// <param name="name">The name to be added.</param>
		internal void AddName(IName name)
		{
			topicData.Names.Add(name);

			if (name is Name)
			{
				((Name) name).Parent = this;
				((Name) name).OnRemove += Name_OnRemove;
			}
		}

		internal void AddOccurrence(Occurrence occurrence)
		{
			occurrence.OnRemove += Occurrence_OnRemove;
			topicData.Occurrences.Add(occurrence);
			occurrence.Parent = this;
		}

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

			topicData.RolesPlayed.Add(role);
		}

		internal Name FindName(Name pattern, bool ignoreParent)
		{
			foreach (Name name in Names)
			{
				if (name == pattern)
				{
					continue;
				}

				if (name.Equals(pattern, ignoreParent))
				{
					return name;
				}
			}

			return null;
		}

		internal Occurrence FindOccurrence(Occurrence pattern, bool ignoreParent)
		{
			foreach (Occurrence occurrence in Occurrences)
			{
				if (occurrence == pattern)
				{
					continue;
				}

				if (occurrence.Equals(pattern, ignoreParent))
				{
					return occurrence;
				}
			}

			return null;
		}

		internal Role FindRole(Role pattern, bool ignoreParent)
		{
			foreach (Role role in RolesPlayed)
			{
				if (role == pattern)
				{
					continue;
				}

				if (role.Equals(pattern, ignoreParent))
				{
					return role;
				}
			}

			return null;
		}

		/// <summary>
		/// Removes a name from the internal list of names for this topic.
		/// </summary>
		/// <param name="name">The name to be added.</param>
		internal void RemoveName(IName name)
		{
			topicData.Names.Remove(name);

			if (name is Name)
			{
				((Name) name).OnRemove -= Name_OnRemove;
			}
		}

		internal void RemoveOccurrence(Occurrence occurrence)
		{
			occurrence.OnRemove -= Occurrence_OnRemove;
			topicData.Occurrences.Remove(occurrence);
			occurrence.Parent = null;
		}

		/// <summary>
		/// Creates a simple occurrence.
		/// </summary>
		/// <param name="type">The type of this occurrence.</param>
		/// <param name="themes">The themes to added to scope.</param>
		/// <returns>The created occurrence.</returns>
		Occurrence CreateOccurrence(ITopic type, IList<ITopic> themes)
		{
			Occurrence occurrence = new Occurrence(this, type);
			AddOccurrence(occurrence);

			if (themes != null)
			{
				occurrence.AddThemes(themes);
			}

			return occurrence;
		}

		void Name_OnRemove(object sender, EventArgs e)
		{
			IName name = sender as IName;

			if (name != null)
			{
				RemoveName(name);
			}
		}

		/// <summary>
		/// Handles the OnRemove event of the Occurrence control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void Occurrence_OnRemove(object sender, EventArgs e)
		{
			if (sender is IOccurrence)
			{
				topicData.Occurrences.Remove((IOccurrence) sender);
			}
		}

		/// <summary>
		/// Removes the role from the <see cref="RolesPlayed"/> list if this instance is a player of the role..
		/// </summary>
		/// <param name="role">The role this topic plays.</param>
		void RemoveRolePlayed(IRole role)
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

			topicData.RolesPlayed.Remove(role);
		}

		/// <summary>
		/// Handles the <see cref="Construct.OnRemove"/> event of the RolePlayed control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void RolePlayed_OnRemove(object sender, EventArgs e)
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
		void RolePlayed_OnRolePlayerChanges(object sender, RolePlayerChangedEventArgs e)
		{
			if (sender != null && sender is IRole &&
			    e != null && e.OldPlayer == this)
			{
				RemoveRolePlayed((IRole) sender);
			}
		}
	}
}
