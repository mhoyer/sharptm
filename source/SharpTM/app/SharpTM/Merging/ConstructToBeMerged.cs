// <copyright file="ConstructToBeMerged.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Merging
{
	public class ConstructToBeMerged : IToBeMerged<IConstruct>
	{
		public ConstructToBeMerged(IConstruct construct)
		{
			ToBeMerged = construct;
		}

		/// <summary>
		/// Gets the construct that should be merged using <see cref="IToBeMerged{T}.Into"/> method.
		/// </summary>
		/// <value>The construct to be merged.</value>
		public IConstruct ToBeMerged
		{
			get;
			private set;
		}

		public void Into(IConstruct target)
		{
			ILocator[] itemIdentifiers = new ILocator[ToBeMerged.ItemIdentifiers.Count];
			ToBeMerged.ItemIdentifiers.CopyTo(itemIdentifiers, 0);

			foreach (ILocator locator in itemIdentifiers)
			{
				ToBeMerged.RemoveItemIdentifier(locator);
				target.AddItemIdentifier(locator);
			}
		}
	}
}