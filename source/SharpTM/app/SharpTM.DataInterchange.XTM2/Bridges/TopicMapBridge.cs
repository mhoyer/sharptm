// <copyright file="TopicMapBridge.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.Mapper.FromDTO;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.Bridges
{
	public class TopicMapBridge : IBridge<TopicMapDTO, ITopicMap>
	{
		public TopicMapBridge(ITopicMapSystem topicMapSystem)
		{
			if (topicMapSystem == null)
			{
				throw new ArgumentNullException("topicMapSystem");
			}
			TopicMapSystem = topicMapSystem;
		}

		public ITopicMapSystem TopicMapSystem { get; private set; }

		public ITopicMap Map(TopicMapDTO source)
		{
			return TopicMapFromDTO.Create(TopicMapSystem, source);
		}

		public TopicMapDTO Map(ITopicMap source)
		{
			throw new NotImplementedException();

			// var mapper = new TopicMapToDTO();
			// return mapper.Create(source);
		}
	}
}