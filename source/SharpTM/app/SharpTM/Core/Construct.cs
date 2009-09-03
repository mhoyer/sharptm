// <copyright file="Construct.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using System.Collections.ObjectModel;
using Pixelplastic.TopicMaps.SharpTM.Core.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Base class for all Topic Maps constructs.
	/// </summary>
	public abstract class Construct : IConstruct
	{
		readonly ConstructDTO constructDTO;

		// TEMP ***************
		[Obsolete]
		protected Construct(IConstruct parent, ITopicMap topicMap)
			: this (new TopicDTO(), parent, topicMap) {}
		// TEMP ***************

		/// <summary>
		/// Initializes a new instance of the <see cref="Construct"/> class.
		/// </summary>
		/// <param name="dto">The DTO for data storage.</param>
		/// <param name="parent">The parent of this instance.</param>
		/// <param name="topicMap">The topic map this instance is part of.</param>
		protected Construct(ConstructDTO dto, IConstruct parent, ITopicMap topicMap)
		{
			if (dto == null) throw new ArgumentNullException("dto");

			if (topicMap == null)
			{
				if (this is ITopicMap)
				{
					TopicMap = this as ITopicMap;
				}
				else
				{
					throw new ArgumentNullException("topicMap");
				}
			}

			constructDTO = dto;
			Parent = parent;
			TopicMap = topicMap ?? TopicMap;
		}

		/// <summary>
		/// Occurs when this instance was removed.
		/// </summary>
		public event EventHandler OnRemove;

		#region IConstruct properties
		/// <summary>
		///     Gets the identifier of this construct.
		///     This property has no representation in the Topic Maps - Data Model.
		/// </summary>
		/// <returns>
		///     An identifier which identifies this construct uniquely within a topic map.
		/// </returns>
		public string Id
		{
			get { return constructDTO.Id; }
		}

		/// <summary>
		///     Gets the item identifiers of this Topic Maps construct.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable set of <see cref="T:TMAPI.Net.Core.ILocator"/>s representing the item identifiers.
		/// </returns>
		public ReadOnlyCollection<ILocator> ItemIdentifiers
		{
			get { return constructDTO.ItemIdentifiers; }
		}

		/// <summary>
		///     Gets the parent of this construct.
		///     This method returns <c>null</c> if this construct is a <see cref="T:TMAPI.Net.Core.ITopicMap"/> instance.
		/// </summary>
		/// <returns>
		///     The parent of this construct or <c>null</c> if the construct is an instance 
		///     of <see cref="T:TMAPI.Net.Core.ITopicMap"/>.
		/// </returns>
		public IConstruct Parent
		{
			get; 
			internal set;
		}

		/// <summary>
		///     Gets the <see cref="TMAPI.Net.Core.ITopicMap"/> instance to which this Topic Maps construct
		///     belongs.
		///     A <see cref="TMAPI.Net.Core.ITopicMap"/> instance returns itself. 
		/// </summary>
		/// <returns>
		///     The <see cref="TMAPI.Net.Core.ITopicMap"/> instance to which this constructs belongs.
		/// </returns>
		public ITopicMap TopicMap
		{
			get; 
			internal set;
		}
		#endregion

		#region IConstruct methods
		/// <summary>
		///     Adds an item identifier.
		///     It is not allowed to have two <see cref="T:TMAPI.Net.Core.IConstruct">constructs</see> in the same 
		///     <see cref="T:TMAPI.Net.Core.ITopicMap"/> with the same item identifier.
		///     If the two objects are <see cref="T:TMAPI.Net.Core.ITopic">topics</see>, then they must be merged.
		///     If at least one of the two objects is not a <see cref="T:TMAPI.Net.Core.ITopic"/>, 
		///     an <see cref="T:TMAPI.Net.Core.IdentityConstraintException"/> must be reported.
		/// </summary>
		/// <param name="itemIdentifier">
		///     The item identifier to be added; must not be <c>null</c>.
		/// </param>
		/// <exception cref="ModelConstraintException">
		///     If the <paramref name="itemIdentifier"/> is <c>null</c>. 
		/// </exception>
		/// <exception cref="IdentityConstraintException">
		///     If another construct has an item identifier which is equal to <paramref name="itemIdentifier"/>.
		/// </exception>
		public void AddItemIdentifier(ILocator itemIdentifier)
		{
			if (itemIdentifier == null)
			{
				throw new ModelConstraintException("Not allowed to add 'null' item identifier.",
				                                   new ArgumentNullException("itemIdentifier"));
			}

			if (ItemIdentifiers.Contains(itemIdentifier))
			{
				return;
			}

			CheckForExistingConstruct(itemIdentifier);
			ITopic existingTopic = TopicMap.GetTopicBySubjectIdentifier(itemIdentifier);

			if (existingTopic != null && existingTopic != this)
			{
				if (!(this is ITopic))
				{
					throw new NotSupportedException(
						String.Format("Cannot add an item identifier '{0}' to a non-topic '{1}', if another topic '{2}' with same subject identifier already exists.", 
							itemIdentifier,
							this,
							existingTopic));
				}

				MergeExistingTopicWithSameSubjectIdentifier(existingTopic, itemIdentifier);
			}
			else
			{
				constructDTO.ItemIdentifiers.Add(itemIdentifier);
			}
		}

		/// <summary>
		///     Deletes this construct from its parent container.
		/// </summary>
		public void Remove()
		{
			if (OnRemove != null)
			{
				OnRemove(this, new EventArgs());
			}
		}

		/// <summary>
		///     Removes an item identifier.
		/// </summary>
		/// <param name="itemIdentifier">
		///     The item identifier to be removed.
		/// </param>
		public void RemoveItemIdentifier(ILocator itemIdentifier)
		{
			constructDTO.ItemIdentifiers.Remove(itemIdentifier);
		}
		#endregion

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		public override string ToString()
		{
			if (ItemIdentifiers.Count > 0)
			{
				return String.Format("{0} ({1})",
				                     Id,
				                     ItemIdentifiers[0]);
			}

			return Id;
		}

		/// <summary>
		/// Checks if there is already a construct with this <paramref name="itemIdentifier"/> existing.
		/// </summary>
		/// <param name="itemIdentifier">The item identifier.</param>
		/// <exception cref="IdentityConstraintException">If this or the found construct is not an <see cref="ITopic"/> instance.</exception>
		void CheckForExistingConstruct(ILocator itemIdentifier)
		{
			IConstruct construct = TopicMap.GetConstructByItemIdentifier(itemIdentifier);

			if (construct != null)
			{
				if (!(construct is ITopic) || !(this is ITopic))
				{
					string message = String.Format(
						"Construct with item identifier {0} still exists in this topic map ({1}).",
						itemIdentifier.Reference,
						TopicMap.Id);

					throw new IdentityConstraintException(message);
				}
			}
		}

		/// <summary>
		/// Merges an <paramref name="existingTopic"/> with same subject identifier as <paramref name="itemIdentifier"/>.
		/// </summary>
		/// <param name="existingTopic">The existing topic.</param>
		/// <param name="itemIdentifier">The item identifier.</param>
		void MergeExistingTopicWithSameSubjectIdentifier(ITopic existingTopic, ILocator itemIdentifier)
		{
			if (((TopicMap) TopicMap).TopicMapSystem.GetFeature(Features.AutomaticMerging))
			{
				existingTopic.MergeIn(((ITopic) this));
				existingTopic.AddItemIdentifier(itemIdentifier);
			}
			else
			{
				throw new IdentityConstraintException(
					String.Format("Topic with item identifier {0} already exists in topic map and [automerge] is not enabled.",
					              itemIdentifier));
			}
		}
	}
}