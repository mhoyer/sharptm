// <copyright file="TopicMapBridge.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts;
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.ToDTO;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Bridges
{
	public class TopicMapBridge : IBridge<TopicMapDTO, ITopicMap>
	{
		public ITopicMapSystem TopicMapSystem { get; private set; }

		public TopicMapBridge(ITopicMapSystem topicMapSystem)
		{
			TopicMapSystem = topicMapSystem;
		}

		public ITopicMap Map(TopicMapDTO source)
		{
			return TopicMapFromDTO.Create(TopicMapSystem, source);
		}

		public TopicMapDTO Map(ITopicMap source)
		{
			throw new NotImplementedException();

			var mapper = new TopicMapToDTO();
			// return mapper.Create(source);
		}
	}

}