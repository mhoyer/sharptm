// <copyright file="TopicMap.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Pixelplastic.TopicMaps.SharpTM.Index;
using Pixelplastic.TopicMaps.SharpTM.Merging;
using TMAPI.Net.Core;
using TMAPI.Net.Index;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	///     Represents a 
	///     <a href="http://www.isotopicmaps.org/sam/sam-model/#d0e657">topic map item</a>.
	/// </summary>
	public class TopicMap : Construct, ITopicMap
	{
		/// <summary>
		/// Represents the current list of <see cref="IAssociation">associations</see>.
		/// </summary>
		readonly List<IAssociation> associations;

		/// <summary>
		/// Represents the current list of <see cref="IConstruct">constructs</see>.
		/// </summary>
		readonly List<IConstruct> constructs;

		/// <summary>
		/// Represents the current instance of <see cref="ILiteralIndex"/>.
		/// </summary>
		readonly ILiteralIndex literalIndex;

		/// <summary>
		/// Represents the current instance of <see cref="Reifiable"/> construct helper.
		/// </summary>
		readonly Reifiable reifiable;

		/// <summary>
		/// Represents the current instance of <see cref="IScopedIndex"/>.
		/// </summary>
		readonly IScopedIndex scopedIndex;

		/// <summary>
		/// Represents the current list of <see cref="ITopic">topics</see>.
		/// </summary>
		readonly List<ITopic> topics;

		/// <summary>
		/// Represents the current instance of <see cref="ITypeInstanceIndex"/>.
		/// </summary>
		readonly ITypeInstanceIndex typedIndex;

		/// <summary>
		/// Initializes a new instance of the <see cref="TopicMap"/> class.
		/// </summary>
		/// <param name="topicMapSystem">The topic map system containing this instance.</param>
		/// <param name="itemIdentifier">The item identifier.</param>
		internal TopicMap(TopicMapSystem topicMapSystem, ILocator itemIdentifier)
			: base(null, null)
		{
			associations = new List<IAssociation>();
			topics = new List<ITopic>();
			constructs = new List<IConstruct>();

			AddItemIdentifier(itemIdentifier);
			TopicMapSystem = topicMapSystem;
			reifiable = new Reifiable(this);

			// TODO How to handle enableAutoUpdate parameter? app.config?
			literalIndex = new LiteralIndex(topicMapSystem, false);
			scopedIndex = new ScopedIndex(topicMapSystem, false);
			typedIndex = new TypedInstanceIndex(topicMapSystem, false);
		}

		#region ITopicMap properties
		/// <summary>
		///     Gets all <see cref="T:TMAPI.Net.Core.IAssociation"/>s contained in this topic map.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable set of <see cref="T:TMAPI.Net.Core.IAssociation"/>s.
		/// </returns>
		public ReadOnlyCollection<IAssociation> Associations
		{
			get
			{
				return associations.AsReadOnly();
			}
		}

		/// <summary>
		///     Gets <c>null</c> since topic maps do not have a parent.
		/// </summary>
		/// <returns>
		///     <c>null</c> since topic maps do not have a parent.
		/// </returns>
		public new IConstruct Parent
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		///     Gets or sets the reifier of this construct.
		/// </summary>
		/// <remarks>
		///     <list type="bullet">
		///         <item>If this construct is not reified <c>null</c> is returned.</item>
		///         <item>If the reifier is set to <c>null</c> an existing reifier should be removed.</item>
		///         <item>The reifier of this construct MUST NOT reify another information item.</item>
		///     </list>
		/// </remarks>
		public ITopic Reifier
		{
			get
			{
				return reifiable.Reifier;
			}
			set
			{
				reifiable.Reifier = value;
			}
		}

		/// <summary>
		///     Gets all <see cref="TMAPI.Net.Core.ITopic">topics</see> contained in this topic map.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable set of <see cref="TMAPI.Net.Core.ITopic">topics</see>.
		/// </returns>
		public ReadOnlyCollection<ITopic> Topics
		{
			get
			{
				return topics.AsReadOnly();
			}
		}
		#endregion

		/// <summary>
		/// Gets the <see cref="ITopicMapSystem"/> containing this instance.
		/// </summary>
		/// <value>The topic map system.</value>
		public TopicMapSystem TopicMapSystem
		{
			get;
			private set;
		}

		#region ITopicMap methods
		/// <summary>
		///     Closes use of this topic map instance.
		///     This method should be invoked by the application once it is finished using this topic map instance.
		///     Release any resources required for the <c>TopicMap</c> instance or any of 
		///     the <see cref="T:TMAPI.Net.Core.IConstruct"/> instances contained by this instance.
		/// </summary>
		public void Close()
		{
			associations.Clear();
			constructs.Clear();
			topics.Clear();
		}

		/// <summary>
		///     Creates an <see cref="T:TMAPI.Net.Core.IAssociation"/> in this topic map with the specified 
		///     <paramref name="associationType"/> and <paramref name="initialThemes"/>.
		/// </summary>
		/// <param name="associationType">
		///     The association type, MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="initialThemes">
		///     An optional array of themes, MUST NOT be <c>null</c>.
		///     If the array's length is <c>0</c>, the association will be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IAssociation"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If either the <paramref name="associationType"/> or <paramref name="initialThemes"/> is <c>null</c>.
		/// </exception>
		public IAssociation CreateAssociation(ITopic associationType, params ITopic[] initialThemes)
		{
			if (initialThemes == null)
			{
				throw new ModelConstraintException(
					"The optional initial themes of an association MUST NOT be null.",
					new ArgumentNullException("initialThemes"));
			}

			return CreateAssociation(associationType, new List<ITopic>(initialThemes));
		}

		/// <summary>
		///     Creates an <see cref="T:TMAPI.Net.Core.IAssociation"/> in this topic map with the specified 
		///     <paramref name="associationType"/> and <paramref name="initialThemes"/>.
		/// </summary>
		/// <param name="associationType">
		///     The association type, MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="initialThemes">
		///     A collection of themes or <c>null</c> if the association should be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IAssociation"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If the <paramref name="associationType"/> is <c>null</c>.
		/// </exception>
		public IAssociation CreateAssociation(ITopic associationType, IList<ITopic> initialThemes)
		{
			Association association = new Association(this, associationType, initialThemes);
			association.OnRemove += Association_OnRemove;
			associations.Add(association);

			constructs.Add(association);

			return association;
		}

		/// <summary>
		///     Returns a <see cref="T:TMAPI.Net.Core.ILocator"/> instance representing the specified IRI 
		///     <paramref name="reference"/>.  
		///     The specified IRI <paramref name="reference"/> is assumed to be absolute.
		/// </summary>
		/// <param name="reference">
		///     A string which uses the IRI notation.
		/// </param>
		/// <returns>
		///     A <see cref="T:TMAPI.Net.Core.ILocator"/> representing the IRI <paramref name="reference"/>.
		/// </returns>
		public ILocator CreateLocator(string reference)
		{
			return TopicMapSystem.CreateLocator(reference);
		}

		/// <summary>
		///     Returns a <see cref="T:TMAPI.Net.Core.ITopic"/> instance with an automatically generated item identifier.
		///     This method returns never an existing <see cref="T:TMAPI.Net.Core.ITopic"/> but creates a new one 
		///     with an automatically generated item identifier.
		///     How that item identifier is generated depends on the implementation.
		/// </summary>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.ITopic"/> instance with an automatically generated item identifier.
		/// </returns>
		public ITopic CreateTopic()
		{
			ILocator initialTopicMapItemIdentifier = ItemIdentifiers[0];
			ILocator uniqueTopicItemIdentifier = initialTopicMapItemIdentifier.Resolve(Guid.NewGuid().ToString());

			return CreateTopicByItemIdentifier(uniqueTopicItemIdentifier);
		}

		/// <summary>
		/// Returns a <see cref="T:TMAPI.Net.Core.ITopic"/> instance with the specified item identifier.
		/// This method returns either an existing <see cref="T:TMAPI.Net.Core.ITopic"/> or creates a new 
		/// <see cref="T:TMAPI.Net.Core.ITopic"/> instance with the specified item identifier.
		/// If a topic with the specified item identifier exists in the topic map, that topic is returned. 
		/// 
		/// If a topic with a subject identifier equals to the specified item identifier exists, the specified item identifier 
		/// is added to that topic and the topic is returned. 
		/// 
		/// If neither a topic with the specified item identifier nor with a subject identifier equals to the item identifier 
		/// exists, a topic with the item identifier is created.
		/// </summary>
		/// <param name="itemIdentifier">
		///     The item identifier the topic should contain.
		/// </param>
		/// <returns>
		///     A <see cref="T:TMAPI.Net.Core.ITopic"/> instance with the specified item identifier.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If the item identifier <paramref name="itemIdentifier"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="IdentityConstraintException">
		///     If an other <see cref="T:TMAPI.Net.Core.IConstruct"/> with the specified item identifier exists which is 
		///     not a <see cref="T:TMAPI.Net.Core.ITopic"/>. 
		/// </exception>
		public ITopic CreateTopicByItemIdentifier(ILocator itemIdentifier)
		{
			if (itemIdentifier == null)
			{
				throw new ModelConstraintException(
					"Topics need at least one item identifier, subject identifier or subject locator.",
					new ArgumentNullException("itemIdentifier"));
			}

			// lookup constructs by item identifier
			IConstruct construct = GetConstructByItemIdentifier(itemIdentifier);

			if (construct != null)
			{
				if (construct is ITopic)
				{
					return construct as ITopic;
				}

				string message = String.Format(
					"Another construct of type {0} with item identifier {1} still exists in topic map ({2}).",
					construct.GetType().Name,
					itemIdentifier.Reference,
					Id);

				throw new IdentityConstraintException(message);
			}

			// lookup topics by subject identifier
			ITopic foundTopicBySubjectIdentifier = GetTopicBySubjectIdentifier(itemIdentifier);

			if (foundTopicBySubjectIdentifier != null)
			{
				foundTopicBySubjectIdentifier.AddItemIdentifier(itemIdentifier);

				return foundTopicBySubjectIdentifier;
			}

			// create new topic with this item identifier
			Topic topic = new Topic(this);
			topic.AddItemIdentifier(itemIdentifier);
			AddTopic(topic);

			return topic;
		}

		/// <summary>
		/// Returns a <see cref="T:TMAPI.Net.Core.ITopic"/> instance with the specified subject identifier.
		/// This method returns either an existing <see cref="T:TMAPI.Net.Core.ITopic"/> or creates a new 
		/// <see cref="T:TMAPI.Net.Core.ITopic"/> instance with the specified subject identifier.
		/// 
		/// If a topic with the specified subject identifier exists in the topic map, that topic is returned. 
		/// 
		/// If a topic with an item identifier equals to the specified subject identifier exists, the specified 
		/// subject identifier is added to that topic and the topic is returned. 
		/// 
		/// If neither a topic with the specified subject identifier nor with an item identifier equals to the
		/// subject identifier exists, a topic with the subject identifier is created.
		/// </summary>
		/// <param name="subjectIdentifier">
		///     The subject identifier the topic should contain.
		/// </param>
		/// <returns>
		///     A <see cref="T:TMAPI.Net.Core.ITopic"/> instance with the specified subject identifier.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If the subject identifier <paramref name="subjectIdentifier"/> is <c>null</c>.
		/// </exception>
		public ITopic CreateTopicBySubjectIdentifier(ILocator subjectIdentifier)
		{
			if (subjectIdentifier == null)
			{
				throw new ModelConstraintException(
					"Topics need at least one item identifier, subject identifier or subject locator.",
					new ArgumentNullException("subjectIdentifier"));
			}

			// lookup topics by subject identifier
			ITopic foundTopicBySubjectIdentifier = GetTopicBySubjectIdentifier(subjectIdentifier);

			if (foundTopicBySubjectIdentifier != null)
			{
				return foundTopicBySubjectIdentifier;
			}

			// lookup constructs by item identifier
			ITopic foundTopicByItemIdentifier = GetTopicByItemIdentifier(subjectIdentifier);

			if (foundTopicByItemIdentifier != null)
			{
				foundTopicByItemIdentifier.AddSubjectIdentifier(subjectIdentifier);

				return foundTopicByItemIdentifier;
			}

			// create new topic with this item identifier
			Topic topic = new Topic(this);
			topic.OnRemove += Topic_OnRemove;
			topic.AddSubjectIdentifier(subjectIdentifier);

			topics.Add(topic);
			constructs.Add(topic);

			return topic;
		}

		/// <summary>
		///     Returns a <see cref="T:TMAPI.Net.Core.ITopic"/> instance with the specified subject locator.
		///     This method returns either an existing <see cref="T:TMAPI.Net.Core.ITopic"/> or creates a new 
		///     <see cref="T:TMAPI.Net.Core.ITopic"/> instance with the specified subject locator.
		/// </summary>
		/// <param name="subjectLocator">
		///     The subject locator the topic should contain.
		/// </param>
		/// <returns>
		///     A <see cref="T:TMAPI.Net.Core.ITopic"/> instance with the specified subject locator.
		/// </returns>
		///  <exception cref="ModelConstraintException">
		///     If the subject locator <paramref name="subjectLocator"/> is <c>null</c>.
		/// </exception>
		public ITopic CreateTopicBySubjectLocator(ILocator subjectLocator)
		{
			if (subjectLocator == null)
			{
				throw new ModelConstraintException(
					"Topics need at least one item identifier, subject identifier or subject locator.",
					new ArgumentNullException("subjectLocator"));
			}

			// lookup topics by subject locator
			ITopic foundTopicBySubjectLocator = GetTopicBySubjectLocator(subjectLocator);

			if (foundTopicBySubjectLocator != null)
			{
				return foundTopicBySubjectLocator;
			}

			// create new topic with this item identifier
			Topic topic = new Topic(this);
			topic.OnRemove += Topic_OnRemove;
			topic.AddSubjectLocator(subjectLocator);

			topics.Add(topic);
			constructs.Add(topic);

			return topic;
		}

		/// <summary>
		///     Returns a <see cref="T:TMAPI.Net.Core.IConstruct"/> by its (system specific) identifier.
		/// </summary>
		/// <param name="id">
		///     The identifier of the construct to be returned.
		/// </param>
		/// <returns>
		///     The construct with the specified id or <c>null</c> if such a construct is unknown.
		/// </returns>
		public IConstruct GetConstructById(string id)
		{
			if (Id == id)
			{
				return this;
			}

			foreach (IConstruct construct in constructs)
			{
				if (construct.Id == id)
				{
					return construct;
				}
			}

			return null;
		}

		/// <summary>
		///     Returns a <see cref="T:TMAPI.Net.Core.IConstruct"/> by its item identifier.
		/// </summary>
		/// <param name="itemIdentifier">
		///     The item identifier of the construct to be returned.
		/// </param>
		/// <returns>
		///     A construct with the specified item identifier or <c>null</c> if no such construct exists in the topic map.
		/// </returns>
		public IConstruct GetConstructByItemIdentifier(ILocator itemIdentifier)
		{
			if (ItemIdentifiers.Contains(itemIdentifier))
			{
				return this;
			}

			foreach (IConstruct construct in constructs)
			{
				if (construct.ItemIdentifiers.Contains(itemIdentifier))
				{
					return construct;
				}
			}

			return null;
		}

		/// <summary>
		///     Returns an index instance for this topic map using the specified generic type.
		/// </summary>
		/// <typeparam name="T">
		///     The data type of the index for this topic map.
		/// </typeparam>
		/// <returns>
		///     The index instance for this topic map.
		/// </returns>
		/// <exception cref="NotSupportedException">
		///     If the implementation does not support indices or if the specified index is unsupported.
		/// </exception>
		public T GetIndex<T>() where T : class, IIndex
		{
			if (typeof(ILiteralIndex).IsAssignableFrom(typeof(T)))
			{
				return (T) literalIndex;
			}

			if (typeof(IScopedIndex).IsAssignableFrom(typeof(T)))
			{
				return (T) scopedIndex;
			}

			if (typeof(ITypeInstanceIndex).IsAssignableFrom(typeof(T)))
			{
				return (T) typedIndex;
			}

			throw new TMAPIException("Unable to get index.",
			                         new NotSupportedException(
			                         	String.Format("Implementation does not support implementation of {0}", typeof(T))));
		}

		/// <summary>
		///     Returns a topic by its subject identifier.
		/// </summary>
		/// <param name="subjectIdentifier">
		///     The subject identifier of the topic to be returned.
		/// </param>
		/// <returns>
		///     A topic with the specified subject identifier or <c>null</c> if no such topic exists in the topic map.
		/// </returns>
		public ITopic GetTopicBySubjectIdentifier(ILocator subjectIdentifier)
		{
			foreach (ITopic topic in topics)
			{
				if (topic.SubjectIdentifiers.Contains(subjectIdentifier))
				{
					return topic;
				}
			}

			return null;
		}

		/// <summary>
		///     Returns a topic by its subject locator.
		/// </summary>
		/// <param name="subjectLocator">
		///     The subject locator of the topic to be returned.
		/// </param>
		/// <returns>
		///     A topic with the specified subject locator or <c>null</c> if no such topic exists in the topic map.
		/// </returns>
		public ITopic GetTopicBySubjectLocator(ILocator subjectLocator)
		{
			foreach (ITopic topic in topics)
			{
				if (topic.SubjectLocators.Contains(subjectLocator))
				{
					return topic;
				}
			}

			return null;
		}

		/// <summary>
		///     Merges the topic map <paramref name="other"/> into this topic map.
		///     All <see cref="T:TMAPI.Net.Core.ITopic"/>s and <see cref="T:TMAPI.Net.Core.IAssociation"/>s and all of 
		///     their contents in <paramref name="other"/> will be added to this topic map.
		///     All information items in <paramref name="other"/> will be merged into this topic map as defined by the 
		///     <a href="http://www.isotopicmaps.org/sam/sam-model/#d0e1862">Topic Maps - Data Model (TMDM) merging rules</a>.
		///     The merge process will not modify <paramref name="other"/> in any way.
		/// </summary>
		/// <param name="other">
		///     The topic map to be merged with this topic map instance.
		/// </param>
		public void MergeIn(ITopicMap other)
		{
			Merge.TopicMap(other).Into(this);
		}
		#endregion

		/// <summary>
		/// Gets the topic by item identifier.
		/// </summary>
		/// <remarks>
		/// Should be used instead of <see cref="GetConstructByItemIdentifier"/> for better performance if 
		/// construct to search for is a <see cref="ITopic"/>.
		/// </remarks>
		/// <param name="itemIdentifier">The item identifier.</param>
		/// <returns></returns>
		public ITopic GetTopicByItemIdentifier(ILocator itemIdentifier)
		{
			foreach (ITopic topic in topics)
			{
				if (topic.ItemIdentifiers.Contains(itemIdentifier))
				{
					return topic;
				}
			}

			return null;
		}

		/// <summary>
		/// Adds the construct to current list of constructs.
		/// </summary>
		/// <param name="construct">The construct to be added.</param>
		internal void AddConstruct(IConstruct construct)
		{
			constructs.Add(construct);
		}

		internal void AddTopic(ITopic topic)
		{
			if (topic is Topic)
			{
				((Topic) topic).OnRemove += Topic_OnRemove;
				((Topic) topic).Parent = this;
			}

			topics.Add(topic);
			constructs.Add(topic);
		}

		internal Topic FindDuplicate(Topic duplicatePattern)
		{
			foreach (Topic topic in topics)
			{
				if (topic == duplicatePattern)
				{
					continue;
				}

				if (topic.Equals(duplicatePattern))
				{
					return topic;
				}
			}

			return null;
		}

		/// <summary>
		/// Removes the construct from current list of constructs.
		/// </summary>
		/// <param name="construct">The construct to be removed.</param>
		internal void RemoveConstruct(IConstruct construct)
		{
			constructs.Remove(construct);
		}

		internal void RemoveTopic(ITopic topic)
		{
			if (topic != null)
			{
				if (topic is Topic)
				{
					((Topic) topic).OnRemove -= Topic_OnRemove;
					((Topic) topic).Parent = null;
				}

				topics.Remove(topic);
				constructs.Remove(topic);
			}
		}

		/// <summary>
		/// Handles the <see cref="Construct.OnRemove"/> event of an <see cref="Association"/> instance.
		/// </summary>
		/// <param name="sender">The source association that triggers the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void Association_OnRemove(object sender, EventArgs e)
		{
			if (sender is IAssociation)
			{
				associations.Remove((IAssociation) sender);
			}
		}

		/// <summary>
		/// Handles the <see cref="Construct.OnRemove"/> event of a <see cref="Topic"/> instance.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void Topic_OnRemove(object sender, EventArgs e)
		{
			ITopic topic = sender as ITopic;
			RemoveTopic(topic);
		}
	}
}