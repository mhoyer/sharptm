// <copyright file="With_TopicMap.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using TMAPI.Net.Core;
using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests
{
	public abstract class With_TopicMap : With_TopicMapSystem
	{
		protected static ITopicMap topicMap;
		public const string TOPIC_MAP_SID = "http://pixelplastic.de/topicMap";

		Given a_topic_map = ()
			=> topicMap = topicMapSystem.CreateTopicMap(TOPIC_MAP_SID);
	}
}