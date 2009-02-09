// <copyright file="AssociationToBeMerged.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Merging
{
	public class AssociationToBeMerged : IToBeMerged<IAssociation>
	{
		public AssociationToBeMerged(IAssociation association)
		{
			ToBeMerged = association;
		}

		/// <summary>
		/// Gets the construct that should be merged using <see cref="IToBeMerged{T}.Into"/> method.
		/// </summary>
		/// <value>The construct to be merged.</value>
		public IAssociation ToBeMerged
		{
			get;
			private set;
		}

		/// <summary>
		/// Merges <see cref="IToBeMerged{T}.ToBeMerged"/> into the specified target.
		/// </summary>
		/// <param name="target">The target to merge in.</param>
		public void Into(IAssociation target)
		{
			Merge.Construct(ToBeMerged).Into(target);
			Merge.Reifiable(ToBeMerged).Into(target);
			Merge.Typed(ToBeMerged).Into(target);
			Merge.Scoped(ToBeMerged).Into(target);

			ToBeMerged.Remove();
		}
	}
}