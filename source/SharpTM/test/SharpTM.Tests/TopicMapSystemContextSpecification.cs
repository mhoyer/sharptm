// <copyright file="TopicMapSystemContextSpecification.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using TMAPI.Net.Core;
using Xunit.BDDExtension;
using STM = Pixelplastic.TopicMaps.SharpTM.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Tests
{
	public abstract class TopicMapSystemContextSpecification : BDDTest
	{
		protected static ITopicMapSystem topicMapSystem;

		Given a_topic_map_system =
			() =>
				{
					TopicMapSystemFactory topicMapSystemFactory =
						TopicMapSystemFactory.NewInstance<Core.TopicMapSystemFactory>();
					topicMapSystem = topicMapSystemFactory.NewTopicMapSystem();
				};
	}
}