// <copyright file="TopicMapData.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System.Collections.Generic;
using TMAPI.Net.Core;
using TMAPI.Net.Index;

namespace Pixelplastic.TopicMaps.SharpTM.Core.DTOs
{
	internal class TopicMapData : ConstructData
	{
		public ReadOnlyCollectionWithLimitedAccess<IAssociation> Associations { get; set; }
		public ReadOnlyCollectionWithLimitedAccess<ITopic> Topics { get; set; }
		public TopicMapSystem TopicMapSystem { get; set; }
		public ITopic Reifier { get; set; }
		public List<IConstruct> Constructs { get; set; }
		public ILiteralIndex LiteralIndex{ get; set; }
		public IScopedIndex ScopedIndex { get; set; }
		public ITypeInstanceIndex TypedIndex { get; set; }

		public TopicMapData()
		{
			Associations = new ReadOnlyCollectionWithLimitedAccess<IAssociation>();
			Topics = new ReadOnlyCollectionWithLimitedAccess<ITopic>();
			Constructs = new List<IConstruct>();
		}
	}
}