// <copyright file="TypedInstanceIndex.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System.Collections.Generic;
using System.Collections.ObjectModel;
#if LOG4NET
using log4net;
#endif
using TMAPI.Net.Core;
using TMAPI.Net.Index;

namespace Pixelplastic.TopicMaps.SharpTM.Index
{
	/// <summary>
	/// Implements the <see cref="ITypeInstanceIndex"/> interface.
	/// </summary>
	public class TypedInstanceIndex : Index, ITypeInstanceIndex
	{
#if LOG4NET
		static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
#endif

		readonly List<ITyped> allTyped;

		/// <summary>
		/// Represents the list of <see cref="ITopic"/>s for <see cref="IAssociation"/> types.
		/// </summary>
		readonly List<ITopic> associationTypes;

		/// <summary>
		/// Represents the list of <see cref="ITopic"/>s for <see cref="IName"/> types.
		/// </summary>
		readonly List<ITopic> nameTypes;

		/// <summary>
		/// Represents the list of <see cref="ITopic"/>s for <see cref="IOccurrence"/> types.
		/// </summary>
		readonly List<ITopic> occurrenceTypes;

		/// <summary>
		/// Represents the list of <see cref="ITopic"/>s for <see cref="IRole"/> types.
		/// </summary>
		readonly List<ITopic> roleTypes;

		/// <summary>
		/// Represents the list of <see cref="ITopic"/>s for <see cref="ITopic"/> types.
		/// </summary>
		readonly List<ITopic> topicTypes;

		/// <summary>
		/// Initializes a new instance of the <see cref="TypedInstanceIndex"/> class.
		/// </summary>
		/// <param name="topicMapSystem">The topic map system this index is based on.</param>
		/// <param name="enableAutoUpdate">if set to <c>true</c> auto update will be enabled.</param>
		public TypedInstanceIndex(ITopicMapSystem topicMapSystem, bool enableAutoUpdate)
			: base(topicMapSystem, enableAutoUpdate)
		{
			associationTypes = new List<ITopic>();
			AssociationTypes = associationTypes.AsReadOnly();

			nameTypes = new List<ITopic>();
			NameTypes = nameTypes.AsReadOnly();

			occurrenceTypes = new List<ITopic>();
			OccurrenceTypes = occurrenceTypes.AsReadOnly();

			roleTypes = new List<ITopic>();
			RoleTypes = roleTypes.AsReadOnly();

			topicTypes = new List<ITopic>();
			TopicTypes = topicTypes.AsReadOnly();

			allTyped = new List<ITyped>();
			AllTyped = allTyped.AsReadOnly();
		}

		/// <summary>
		/// Gets all <see cref="ITyped"/> instances.
		/// </summary>
		/// <value>A list of <see cref="ITyped"/> instances.</value>
		public ReadOnlyCollection<ITyped> AllTyped
		{
			get;
			private set;
		}

		#region ITypeInstanceIndex properties
		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s in the topic map used 
		///     in the type property of <see cref="T:TMAPI.Net.Core.IAssociation"/>s. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
		/// </returns>
		public ReadOnlyCollection<ITopic> AssociationTypes
		{
			get;
			private set;
		}

		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s in the topic map used 
		///     in the type property of <see cref="T:TMAPI.Net.Core.IName"/>s. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
		/// </returns>
		public ReadOnlyCollection<ITopic> NameTypes
		{
			get;
			private set;
		}

		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s in the topic map used 
		///     in the type property of <see cref="T:TMAPI.Net.Core.IOccurrence"/>s. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
		/// </returns>
		public ReadOnlyCollection<ITopic> OccurrenceTypes
		{
			get;
			private set;
		}

		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s in the topic map used 
		///     in the type property of <see cref="T:TMAPI.Net.Core.IRole"/>s. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
		/// </returns>
		public ReadOnlyCollection<ITopic> RoleTypes
		{
			get;
			private set;
		}

		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s in topic map which are used 
		///     as type in an "type-instance"-relationship. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
		/// </returns>
		/// <remarks>
		///     Implementations may return only those topics which are member of the <c>types</c> 
		///     property of other topics and may ignore <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-types">
		///     type-instance</a> relationships which are modeled as association. 
		///     Further, <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-subtypes">supertype-subtype</a>
		///     relationships may also be ignored.
		/// </remarks>
		public ReadOnlyCollection<ITopic> TopicTypes
		{
			get;
			private set;
		}
		#endregion

		#region ITypeInstanceIndex methods
		/// <summary>
		/// Synchronize the index with data in the topic map.
		/// </summary>
		public override void Reindex()
		{
#if LOG4NET
			log.InfoFormat("Refreshing the index.");
#endif

			associationTypes.Clear();
			nameTypes.Clear();
			occurrenceTypes.Clear();
			roleTypes.Clear();
			topicTypes.Clear();
			allTyped.Clear();

			foreach (ILocator locator in TopicMapSystem.Locators)
			{
				ReindexTopicMap(TopicMapSystem.GetTopicMap(locator));
			}
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.IAssociation"/>s in the topic map whose 
		///     type property equals <paramref name="type"/>. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="type">
		///     The type of the <see cref="T:TMAPI.Net.Core.IAssociation"/>s to be returned.
		/// </param>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IAssociation"/>s.
		/// </returns>
		public ReadOnlyCollection<IAssociation> GetAssociations(ITopic type)
		{
			List<IAssociation> foundAssociations = new List<IAssociation>();

			foreach (ILocator locator in TopicMapSystem.Locators)
			{
				ITopicMap topicMap = TopicMapSystem.GetTopicMap(locator);

				foreach (IAssociation association in topicMap.Associations)
				{
					if (association.Type == type)
					{
						foundAssociations.Add(association);
					}
				}
			}

			return foundAssociations.AsReadOnly();
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.IName"/>s in the topic map whose 
		///     type property equals <paramref name="type"/>. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="type">
		///     The type of the <see cref="T:TMAPI.Net.Core.IName"/>s to be returned.
		/// </param>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IName"/>s.
		/// </returns>
		public ReadOnlyCollection<IName> GetNames(ITopic type)
		{
			List<IName> foundNames = new List<IName>();

			foreach (ILocator locator in TopicMapSystem.Locators)
			{
				ITopicMap topicMap = TopicMapSystem.GetTopicMap(locator);

				foreach (ITopic topic in topicMap.Topics)
				{
					foreach (IName name in topic.Names)
					{
						if (name.Type == type)
						{
							foundNames.Add(name);
						}
					}
				}
			}

			return foundNames.AsReadOnly();
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s in the topic map whose 
		///     type property equals <paramref name="type"/>. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="type">
		///     The type of the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s to be returned.
		/// </param>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IOccurrence"/>s.
		/// </returns>
		public ReadOnlyCollection<IOccurrence> GetOccurrences(ITopic type)
		{
			List<IOccurrence> foundOccurrences = new List<IOccurrence>();

			foreach (ILocator locator in TopicMapSystem.Locators)
			{
				ITopicMap topicMap = TopicMapSystem.GetTopicMap(locator);

				foreach (ITopic topic in topicMap.Topics)
				{
					foreach (IOccurrence occurrence in topic.Occurrences)
					{
						if (occurrence.Type == type)
						{
							foundOccurrences.Add(occurrence);
						}
					}
				}
			}

			return foundOccurrences.AsReadOnly();
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.IRole"/>s in the topic map whose 
		///     type property equals <paramref name="type"/>. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="type">
		///     The type of the <see cref="T:TMAPI.Net.Core.IRole"/>s to be returned.
		/// </param>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IRole"/>s.
		/// </returns>
		public ReadOnlyCollection<IRole> GetRoles(ITopic type)
		{
			List<IRole> foundRoles = new List<IRole>();

			foreach (ILocator locator in TopicMapSystem.Locators)
			{
				foreach (IAssociation association in TopicMapSystem.GetTopicMap(locator).Associations)
				{
					foreach (IRole role in association.Roles)
					{
						if (role.Type == type)
						{
							foundRoles.Add(role);
						}
					}
				}
			}

			return foundRoles.AsReadOnly();
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.ITopic"/>s which are an instance of 
		///     the specified <paramref name="type"/>. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="type">
		///     The type of the <see cref="T:TMAPI.Net.Core.ITopic"/>s to be returned.
		/// </param>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
		/// </returns>
		/// <remarks>
		///     Implementations may return only those topics whose <tt>types</tt> property
		///     contains the type and may ignore <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-types">
		///     type-instance</a> relationships which are modeled as association. Further, 
		///     <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-subtypes">supertype-
		///     subtype</a> relationships may also be ignored. 
		/// </remarks>
		public ReadOnlyCollection<ITopic> GetTopics(ITopic type)
		{
			return GetTopics(new[] { type }, true);
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.ITopic"/>s in the topic map whose type 
		///     property equals one of those <paramref name="types"/> at least. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="types">
		///     Types of the <see cref="T:TMAPI.Net.Core.ITopic"/>s to be returned.
		/// </param>
		/// <param name="matchAll">
		///     If <c>true</c>, a topic must be an instance of all <paramref name="types"/>, 
		///     if <c>false</c> the topic must be an instance of one type at least.
		/// </param>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.ITopic"/>s.
		/// </returns>
		/// <remarks>
		///     Implementations may return only those topics whose <tt>types</tt> property
		///     contains the type and may ignore <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-types">
		///     type-instance</a> relationships which are modeled as association. Further, 
		///     <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-subtypes">supertype-
		///     subtype</a> relationships may also be ignored. 
		/// </remarks>
		public ReadOnlyCollection<ITopic> GetTopics(ITopic[] types, bool matchAll)
		{
			List<ITopic> foundTopics = new List<ITopic>();

			foreach (ILocator locator in TopicMapSystem.Locators)
			{
				foreach (ITopic topic in TopicMapSystem.GetTopicMap(locator).Topics)
				{
					if (AreTypesMatchingTypes(topic.Types, types, matchAll))
					{
						foundTopics.Add(topic);
					}
				}
			}

			return foundTopics.AsReadOnly();
		}
		#endregion

		/// <summary>
		/// Gets all <see cref="ITyped"/> instances that are type of <paramref name="type"/>.
		/// </summary>
		/// <param name="type">The type to be used for comparison.</param>
		/// <returns>The filtered list of <see cref="ITyped"/> instances.</returns>
		public ReadOnlyCollection<ITyped> GetTyped(ITopic type)
		{
			List<ITyped> foundTopics = allTyped.FindAll(typed => typed.Type == type);

			return foundTopics.AsReadOnly();
		}

		static bool AreTypesMatchingTypes(ICollection<ITopic> source, ITopic[] lookupTypes, bool matchAll)
		{
			bool matches = matchAll;

			foreach (ITopic type in lookupTypes)
			{
				if (matchAll)
				{
					if ((type != null && !source.Contains(type)) ||
					    (type == null && source.Count > 0))
					{
						matches = false;
						break;
					}
				}
				else
				{
					if ((type == null && source.Count == 0) ||
					    (type != null && source.Contains(type)))
					{
						matches = true;
						break;
					}
				}
			}

			return matches;
		}

		void ReindexTopicMap(ITopicMap topicMap)
		{
			foreach (ITopic topic in topicMap.Topics)
			{
				foreach (ITopic type in topic.Types)
				{
					if (!topicTypes.Contains(type))
					{
						topicTypes.Add(type);
					}
				}

				foreach (IName name in topic.Names)
				{
					if (!nameTypes.Contains(name.Type))
					{
						nameTypes.Add(name.Type);
						allTyped.Add(name);
					}
				}

				foreach (IOccurrence occurrence in topic.Occurrences)
				{
					if (!occurrenceTypes.Contains(occurrence.Type))
					{
						occurrenceTypes.Add(occurrence.Type);
						allTyped.Add(occurrence);
					}
				}
			}

			foreach (IAssociation association in topicMap.Associations)
			{
				if (!associationTypes.Contains(association.Type))
				{
					associationTypes.Add(association.Type);
					allTyped.Add(association);
				}

				foreach (IRole role in association.Roles)
				{
					ITopic type = role.Type;

					if (!roleTypes.Contains(type))
					{
						roleTypes.Add(type);
						allTyped.Add(role);
					}
				}
			}
		}
	}
}