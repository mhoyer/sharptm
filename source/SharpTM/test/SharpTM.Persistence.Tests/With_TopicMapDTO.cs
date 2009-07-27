// <copyright file="With_TopicMap.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests
{
	public abstract class With_TopicMapDTO : With_TopicMapSystem
	{
		public const string TOPIC_MAP_SID = "http://pixelplastic.de/topicMap";
		
		protected static TopicMapDTO topicMapDTO;
		protected static LocatorDTO topicMapIdentifier;

		Given a_topic_map_identifier = () => topicMapIdentifier = new LocatorDTO() { HRef = TOPIC_MAP_SID };

		Given a_topic_map =
			() =>
				{
					topicMapDTO = new TopicMapDTO();
					topicMapDTO.ItemIdentities.Add(topicMapIdentifier);
				};

	}
}