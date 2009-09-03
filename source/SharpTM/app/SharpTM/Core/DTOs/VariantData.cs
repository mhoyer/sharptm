// <copyright file="VariantData.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core.DTOs
{
	public class VariantData : DatatypeAwareData
	{
		public ReadOnlyCollectionWithLimitedAccess<ITopic> MergedScope { get; set; }

		public VariantData()
		{
			MergedScope = new ReadOnlyCollectionWithLimitedAccess<ITopic>();
		}
	}
}