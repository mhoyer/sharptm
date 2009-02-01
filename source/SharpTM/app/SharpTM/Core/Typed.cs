// <copyright file="Typed.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements a helper class for <see cref="ITyped"/> constructs.
	/// </summary>
	internal class Typed
	{
		/// <summary>
		/// Represents the current type of a <see cref="ITyped"/> construct.
		/// </summary>
		private ITopic type;

		/// <summary>
		/// Initializes a new instance of the <see cref="Typed"/> class.
		/// </summary>
		/// <param name="initialType">The initial type.</param>
		internal Typed(ITopic initialType)
		{
			if (initialType == null)
			{
				throw new ModelConstraintException(
					"Type MUST NOT be null for typed construct.",
					new ArgumentNullException("initialType"));
			}

			Type = initialType;
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
		internal ITopic Type
		{
			get
			{
				return type;
			}
			set
			{
				if (value == null)
				{
					throw new ModelConstraintException("Type MUST NOT be null.");
				}

				type = value;
			}
		}
	}
}