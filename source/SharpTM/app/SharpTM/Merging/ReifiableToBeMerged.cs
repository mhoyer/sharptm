// <copyright file="ReifiableToBeMerged.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Merging
{
	public class ReifiableToBeMerged : IToBeMerged<IReifiable>
	{
		public ReifiableToBeMerged(IReifiable reifiable)
		{
			ToBeMerged = reifiable;
		}

		/// <summary>
		/// Gets the construct that should be merged using <see cref="IToBeMerged{T}.Into"/> method.
		/// </summary>
		/// <value>The construct to be merged.</value>
		public IReifiable ToBeMerged
		{
			get;
			private set;
		}

		/// <summary>
		/// Merges <see cref="IToBeMerged{T}.ToBeMerged"/> into the specified target.
		/// </summary>
		/// <param name="target">The target to merge in.</param>
		public void Into(IReifiable target)
		{
			if (ToBeMerged.Reifier != null)
			{
				ITopic deprecatedReifier = ToBeMerged.Reifier;
				ToBeMerged.Reifier = null;

				if (target.Reifier != null)
				{
					// If both A and B have non-null values, the topic items shall be merged, 
					// and the topic item resulting from the merge be set as the value of C's
					// [reifier] property.
					target.Reifier.MergeIn(deprecatedReifier);
				}
				else
				{
					target.Reifier = deprecatedReifier;
				}
			}
		}
	}
}