// <copyright file="Occurrence.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements the <see cref="IOccurrence"/> interface.
	/// </summary>
	public class Occurrence : DatatypeAware, IOccurrence
	{
		/// <summary>
		/// Represents the current instance of <see cref="Typed"/> construct helper.
		/// </summary>
		readonly Typed typed;

		/// <summary>
		/// Initializes a new instance of the <see cref="Occurrence"/> class.
		/// </summary>
		/// <param name="parent">The parent of this instance.</param>
		/// <param name="type">The type of this instance.</param>
		internal Occurrence(ITopic parent, ITopic type)
			: base(parent, parent.TopicMap)
		{
			if (type == null)
			{
				throw new ModelConstraintException(
					"An occurrence type MUST NOT be null.",
					new ArgumentNullException("type"));
			}

			typed = new Typed(type);
		}

		#region IOccurrence properties
		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.ITopic"/> to which this occurrence belongs.
		/// </summary>
		/// <returns>
		///     The <see cref="T:TMAPI.Net.Core.ITopic"/> to which this occurrence belongs.
		/// </returns>
		public new ITopic Parent
		{
			get
			{
				return base.Parent as ITopic;
			}
			internal set
			{
				base.Parent = value;
			}
		}

		/// <summary>
		///     Gets or sets the type of this construct.
		/// </summary>
		/// <exception cref="ModelConstraintException">
		///     If the type is <c>null</c>.
		/// </exception>
		/// <remarks>
		///     Any previous type is overridden.
		/// </remarks>
		public ITopic Type
		{
			get
			{
				return typed.Type;
			}
			set
			{
				typed.Type = value;
			}
		}
		#endregion

		/// <summary>
		/// Determines whether the specified occurrence is equal.
		/// </summary>
		/// <param name="occurrence">The occurrence.</param>
		/// <returns>
		/// 	<c>true</c> if the specified occurrence is equal; otherwise, <c>false</c>.
		/// </returns>
		/// <remarks>
		/// Occurrence items are equal if the values of their [value], [<c>datatype</c>], 
		/// [scope], [type], and [parent] properties are equal. 
		/// </remarks>
		public bool Equals(Occurrence occurrence)
		{
			return Equals(occurrence, false);
		}

		/// <summary>
		/// Determines whether the specified occurrence is equal.
		/// </summary>
		/// <param name="occurrence">The occurrence.</param>
		/// <param name="ignoreParent">if set to <c>true</c> [ignore parent].</param>
		/// <returns>
		/// 	<c>true</c> if the specified occurrence is equal; otherwise, <c>false</c>.
		/// </returns>
		/// <remarks>
		/// Occurrence items are equal if the values of their [value], [<c>datatype</c>], 
		/// [scope], [type], and [parent] properties are equal. 
		/// </remarks>
		public bool Equals(Occurrence occurrence, bool ignoreParent)
		{
			if (occurrence == this)
			{
				return true;
			}

			if (occurrence == null ||
			    occurrence.Value != Value ||
			    occurrence.Scope.Count != Scope.Count ||
			    !occurrence.Datatype.Equals(Datatype) ||
			    !occurrence.Type.Equals(Type) ||
			    (!ignoreParent && !occurrence.Parent.Equals(Parent)))
			{
				return false;
			}

			foreach (ITopic scope in Scope)
			{
				if (!occurrence.Scope.Contains(scope))
				{
					return false;
				}
			}

			return true;
		}
	}
}