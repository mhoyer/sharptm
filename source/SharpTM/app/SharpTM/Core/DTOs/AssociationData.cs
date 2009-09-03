// <copyright file="AssociationData.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core.DTOs
{
	public class AssociationData : ConstructData
	{
		public ReadOnlyCollectionWithLimitedAccess<IRole> Roles { get; set; }
		public ITopic Reifier { get; set; }
		public ITopic Type { get; set; }

		public AssociationData()
		{
			Roles = new ReadOnlyCollectionWithLimitedAccess<IRole>();
		}
	}
}