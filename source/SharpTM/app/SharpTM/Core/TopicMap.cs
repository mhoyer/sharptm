using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMAPI.Net.Core;
using TMAPI.Net.Index;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	///     Represents a 
	///     <a href="http://www.isotopicmaps.org/sam/sam-model/#d0e657">topic map item</a>.
	/// </summary>
	public class TopicMap : Reifiable, ITopicMap
	{
		#region readonly & static fields
		private readonly List<IAssociation> associations;
		private readonly List<IConstruct> constructs;
		private readonly List<ITopic> topics;
		#endregion

		#region constructor logic
		/// <summary>
		/// Initializes a new instance of the <see cref="TopicMap"/> class.
		/// </summary>
		/// <param name="topicMapSystem">The topic map system.</param>
		/// <param name="iri">The iri.</param>
		internal TopicMap(ITopicMapSystem topicMapSystem, ILocator iri)
			: base(topicMapSystem, null, null)
		{
			if (topicMapSystem == null)
			{
				throw new ArgumentNullException("topicMapSystem");
			}

			associations = new List<IAssociation>();
			topics = new List<ITopic>();
			constructs = new List<IConstruct>();

			AddItemIdentifier(iri);
		}
		#endregion

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
			topics.Clear();
		}

		/// <summary>
		///     Creates an <see cref="T:TMAPI.Net.Core.IAssociation"/> in this topic map with the specified 
		///     <paramref name="type"/> and <paramref name="scope"/>.
		/// </summary>
		/// <param name="type">
		///     The association type, MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="scope">
		///     An optional array of themes, MUST NOT be <c>null</c>.
		///     If the array's length is <c>0</c>, the association will be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IAssociation"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If either the <paramref name="type"/> or <paramref name="scope"/> is <c>null</c>.
		/// </exception>
		public IAssociation CreateAssociation(ITopic type, params ITopic[] scope)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		///     Creates an <see cref="T:TMAPI.Net.Core.IAssociation"/> in this topic map with the specified 
		///     <paramref name="type"/> and <paramref name="scope"/>.
		/// </summary>
		/// <param name="type">
		///     The association type, MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="scope">
		///     A collection of themes or <c>null</c> if the association should be in the unconstrained scope.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IAssociation"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If the <paramref name="type"/> is <c>null</c>.
		/// </exception>
		public IAssociation CreateAssociation(ITopic type, IList<ITopic> scope)
		{
			throw new System.NotImplementedException();
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
			throw new System.NotImplementedException();
		}

		/// <summary>
		///     Returns a <see cref="T:TMAPI.Net.Core.ITopic"/> instance with the specified item identifier.
		///     This method returns either an existing <see cref="T:TMAPI.Net.Core.ITopic"/> or creates a new 
		///     <see cref="T:TMAPI.Net.Core.ITopic"/> instance with the specified item identifier.
		///     If a topic with the specified item identifier exists in the topic map, that topic is returned. If a topic 
		///     with a subject identifier equals to the specified item identifier exists, the specified item identifier 
		///     is added to that topic and the topic is returned. If neither a topic with the specified item identifier 
		///     nor with a subject identifier equals to the item identifier exists, a topic with the item identifier is created.
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
			throw new System.NotImplementedException();
		}

		/// <summary>
		///     Returns a <see cref="T:TMAPI.Net.Core.ITopic"/> instance with the specified subject identifier.
		///     This method returns either an existing <see cref="T:TMAPI.Net.Core.ITopic"/> or creates a new 
		///     <see cref="T:TMAPI.Net.Core.ITopic"/> instance with the specified subject identifier.
		///     If a topic with the specified subject identifier exists in the topic map, that topic is returned. If a topic 
		///     with an item identifier equals to the specified subject identifier exists, the specified subject identifier 
		///     is added to that topic and the topic is returned. If neither a topic with the specified subject identifier 
		///     nor with an item identifier equals to the subject identifier exists, a topic with the subject identifier is created.
		/// </summary>
		/// <param name="subjectIdentifier">
		///     The subject identifier the topic should contain.
		/// </param>
		///  <returns>
		///     A <see cref="T:TMAPI.Net.Core.ITopic"/> instance with the specified subject identifier.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If the subject identifier <paramref name="subjectIdentifier"/> is <c>null</c>.
		/// </exception>
		public ITopic CreateTopicBySubjectIdentifier(ILocator subjectIdentifier)
		{
			throw new System.NotImplementedException();
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
			throw new System.NotImplementedException();
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
			if (itemIdentifiers.Contains(itemIdentifier))
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
		public T GetIndex<T>() where T : IIndex
		{
			throw new System.NotImplementedException();
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
			throw new System.NotImplementedException();
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
			throw new System.NotImplementedException();
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
			throw new System.NotImplementedException();
		}
		#endregion
	}
}