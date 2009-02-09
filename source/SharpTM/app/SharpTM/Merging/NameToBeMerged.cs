// <copyright file="NameToBeMerged.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.Core;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Merging
{
	public class NameToBeMerged : IToBeMerged<IName>
	{
		IName targetName;

		public NameToBeMerged(IName name)
		{
			ToBeMerged = name;
		}

		/// <summary>
		/// Gets the construct that should be merged using <see cref="IToBeMerged{T}.Into"/> method.
		/// </summary>
		/// <value>The construct to be merged.</value>
		public IName ToBeMerged
		{
			get;
			private set;
		}

		public void Into(IName target)
		{
			targetName = target;
			Merge.Construct(ToBeMerged).Into(target);
			Merge.Reifiable(ToBeMerged).Into(target);
			Merge.Typed(ToBeMerged).Into(target);

			UnifyVariants();

			ToBeMerged.Remove();
		}

		void UnifyVariants()
		{
			for (int i = 0; i < ToBeMerged.Variants.Count; i++)
			{
				Variant variantToBeMerged = (Variant) ToBeMerged.Variants[i];
				Variant variantTarget = ((Name) targetName).FindVariant(variantToBeMerged, true);

				if (variantTarget != null)
				{
					Merge.Variant(variantToBeMerged).Into(variantTarget);
				}
				else
				{
					((Name) targetName).AddVariant(variantToBeMerged);
				}
			}
		}
	}
}