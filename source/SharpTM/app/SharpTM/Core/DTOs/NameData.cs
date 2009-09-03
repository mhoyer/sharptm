// <copyright file="NameData.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core.DTOs
{
	public class NameData : ConstructData
	{
		public ReadOnlyCollectionWithLimitedAccess<IVariant> Variants { get; set; }
		public ReadOnlyCollectionWithLimitedAccess<ITopic> Scope { get; set; }
		public ITopic Reifier { get; set; }
		public ITopic Type { get; set; }
		public string Value { get; set; }

		public NameData()
		{
			Variants = new ReadOnlyCollectionWithLimitedAccess<IVariant>();
			Scope = new ReadOnlyCollectionWithLimitedAccess<ITopic>();
		}
	}
}