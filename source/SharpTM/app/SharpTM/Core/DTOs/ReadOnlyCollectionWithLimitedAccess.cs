// <copyright file="ReadOnlyCollectionWithLimitedAccess.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Pixelplastic.TopicMaps.SharpTM.Core.DTOs
{
	/// <summary>
	/// Implements a <see cref="ReadOnlyCollection{T}"/>, but provides internal access to list <see cref="ReadOnlyCollection{T}.Items">Items</see>.
	/// </summary>
	/// <typeparam name="T">
	/// </typeparam>
	internal class ReadOnlyCollectionWithLimitedAccess<T> : ReadOnlyCollection<T>, IList<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ReadOnlyCollectionWithLimitedAccess&lt;T&gt;"/> class.
		/// </summary>
		public ReadOnlyCollectionWithLimitedAccess()
			: this(new List<T>())
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ReadOnlyCollectionWithLimitedAccess&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="list">
		/// The list.
		/// </param>
		public ReadOnlyCollectionWithLimitedAccess(IList<T> list)
			: base(list)
		{
		}

		/// <summary>
		/// Determines if an item matches the specified predicate.
		/// </summary>
		/// <param name="match">The match.</param>
		/// <returns></returns>
		public bool Exists(Predicate<T> match)
		{
			foreach (T item in Items)
			{
				if (match.Invoke(item))
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Adds an item to the collection.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <exception cref="T:System.NotSupportedException">
		/// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
		/// </exception>
		public void Add(T item)
		{
			Items.Add(item);
		}

		/// <summary>
		/// Removes the specified item.
		/// </summary>
		/// <param name="item">The item.</param>
		public bool Remove(T item)
		{
			return Items.Remove(item);
		}

		/// <summary>
		/// Removes all items from the <see cref="ICollection{T}"/>.
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// The <see cref="ICollection{T}"/> is read-only.
		/// </exception>
		public void Clear()
		{
			Items.Clear();
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}
	}
}