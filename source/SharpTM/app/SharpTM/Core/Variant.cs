// <copyright file="Variant.cs" company="Pixelplastic">
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
	/// Implements the <see cref="IVariant"/> interface.
	/// </summary>
	public class Variant : DatatypeAware, IVariant
	{
		/// <summary>
		/// Represents the current scope themes.
		/// </summary>
		readonly List<ITopic> mergedScope;

		/// <summary>
		/// Initializes a new instance of the <see cref="Variant"/> class.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <param name="topicMap">The topic map.</param>
		internal Variant(IName parent, ITopicMap topicMap)
			: base(parent, topicMap)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}

			Parent = parent;
			mergedScope = new List<ITopic>();
			MergedScope = mergedScope.AsReadOnly();
			MergeScopes();
		}

		#region IVariant properties
		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.IName"/> to which this variant belongs.
		/// </summary>
		/// <returns>
		///     The <see cref="T:TMAPI.Net.Core.IName"/> to which this variant belongs.
		/// </returns>
		public new IName Parent
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s which define the scope.
		/// An empty set represents the unconstrained scope.
		/// The return value may be empty but must never be <c>null</c>. 
		/// </summary>
		/// <value></value>
		/// <returns>
		/// An unmodifiable set of <see cref="T:TMAPI.Net.Core.ITopic"/>s which define the scope.
		/// </returns>
		public new ReadOnlyCollection<ITopic> Scope
		{
			get
			{
				MergeScopes();
				return MergedScope;
			}
		}
		#endregion

		/// <summary>
		/// Gets the merged scope as <see cref="ReadOnlyCollection{T}"/>.
		/// </summary>
		/// <value>The merged scope.</value>
		internal ReadOnlyCollection<ITopic> MergedScope
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="Variant"/>.
		/// </summary>
		/// <returns>
		/// The <see cref="Construct.Id"/> and the <see cref="DatatypeAware.Value"/>.
		/// </returns>
		public override string ToString()
		{
			return String.Format("{0} \"{1}\"", Id, Value);
		}

		/// <summary>
		/// Determines whether the specified variant equals this instance.
		/// </summary>
		/// <param name="variant">The variant to be compared to.</param>
		/// <returns>
		/// 	<c>true</c> if the specified variant equals; otherwise, <c>false</c>.
		/// </returns>
		/// <remarks>
		/// Variant items are equal if the values of their [value], [datatype], [scope], and [parent] properties are equal.
		/// </remarks>
		public bool Equals(IVariant variant)
		{
			return Equals(variant, false);
		}

		/// <summary>
		/// Determines whether the specified variant equals this instance.
		/// </summary>
		/// <param name="variant">The variant to be compared to.</param>
		/// <param name="ignoreParent">if set to <c>true</c> the <see cref="Parent"/> property will be ignored for comparison.</param>
		/// <returns>
		/// 	<c>true</c> if the specified variant equals; otherwise, <c>false</c>.
		/// </returns>
		/// <remarks>
		/// Variant items are equal if the values of their [value], [<c>datatype</c>], [scope], and [parent] properties are equal.
		/// </remarks>
		public bool Equals(IVariant variant, bool ignoreParent)
		{
			if (variant == this)
			{
				return true;
			}

			if (variant == null ||
			    variant.Scope.Count != Scope.Count ||
			    !variant.Datatype.Equals(Datatype) ||
			    (!ignoreParent && !variant.Parent.Equals(Parent)) ||
			    variant.Value != Value)
			{
				return false;
			}

			foreach (ITopic scope in Scope)
			{
				if (!variant.Scope.Contains(scope))
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Merges the scopes of <see cref="Parent"/> name construct and the current themes 
		/// of this <see cref="IVariant"/> instance.
		/// </summary>
		void MergeScopes()
		{
			// TODO introduce a dirty flag to reduce unnecessary merging.
			mergedScope.Clear();
			mergedScope.AddRange(Parent.Scope);

			foreach (ITopic theme in base.Scope)
			{
				if (mergedScope.Contains(theme))
				{
					continue;
				}

				mergedScope.Add(theme);
			}
		}
	}
}