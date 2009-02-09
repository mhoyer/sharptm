// <copyright file="TypedToBeMerged.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Merging
{
	public class TypedToBeMerged : IToBeMerged<ITyped>
	{
		public TypedToBeMerged(ITyped typed)
		{
			ToBeMerged = typed;
		}

		/// <summary>
		/// Gets the construct that should be merged using <see cref="IToBeMerged{T}.Into"/> method.
		/// </summary>
		/// <value>The construct to be merged.</value>
		public ITyped ToBeMerged
		{
			get;
			private set;
		}

		/// <summary>
		/// Merges <see cref="IToBeMerged{T}.ToBeMerged"/> into the specified target.
		/// </summary>
		/// <param name="target">The target to merge in.</param>
		public void Into(ITyped target)
		{
			if (target.Type != ToBeMerged.Type)
			{
				throw new TMAPIException("Unable to merge typed instances with different types.");
			}
		}
	}
}