// <copyright file="TopicMapDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core.DTOs
{
	internal class TopicMapDTO : ConstructDTO
	{
		public ReadOnlyCollectionWithLimitedAccess<IAssociation> Associations { get; set; }
		public ReadOnlyCollectionWithLimitedAccess<ITopic> Topics { get; set; }
		public ITopic Reifier { get; set; }
		
		public TopicMapDTO()
		{
			Associations = new ReadOnlyCollectionWithLimitedAccess<IAssociation>();
			Topics = new ReadOnlyCollectionWithLimitedAccess<ITopic>();
		}
	}
}