// <copyright file="Construct.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Base class for all Topic Maps constructs.
	/// </summary>
	public abstract class Construct : IConstruct
	{
		/// <summary>
		/// Represents the list of item identifiers for this construct.
		/// </summary>
		private readonly List<ILocator> itemIdentifiers;

		/// <summary>
		/// Initializes a new instance of the <see cref="Construct"/> class.
		/// </summary>
		/// <param name="parent">The parent of this instance.</param>
		/// <param name="topicMap">The topic map this instance is part of.</param>
		protected Construct(IConstruct parent, ITopicMap topicMap)
		{
			itemIdentifiers = new List<ILocator>();
			ItemIdentifiers = itemIdentifiers.AsReadOnly();

			Id = Guid.NewGuid().ToString();

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
			get;
			private set;
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
			get;
			private set;
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
			private set;
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

			if (TopicMap.ItemIdentifiers.Contains(itemIdentifier))
			{
				String message = String.Format(
					"Construct with item identifier {0} still exists in this topic map ({1}).",
					itemIdentifier.Reference,
					TopicMap.Id);

				throw new IdentityConstraintException(message);
			}

			itemIdentifiers.Add(itemIdentifier);
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
			itemIdentifiers.Remove(itemIdentifier);
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
			if (itemIdentifiers.Count > 0)
			{
				return String.Format("{0} ({1})",
				                     Id,
				                     itemIdentifiers[0]);
			}

			return Id;
		}
	}
}
