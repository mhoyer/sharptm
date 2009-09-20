// <copyright file="With_TopicMapSystem.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.Core;
using TMAPI.Net.Core;
using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Tests
{
	public abstract class With_TopicMapSystem : BDDTest
	{
		protected static ITopicMapSystem topicMapSystem;

		Given a_topic_map_system =
			() =>
				{
					TopicMapSystemFactory topicMapSystemFactory =
						TopicMapSystemFactory.NewInstance<SharpTMSystemFactory>();

					if (topicMapSystemFactory.HasFeature(Features.AutomaticMerging))
					{
						topicMapSystemFactory.SetFeature(Features.AutomaticMerging, true);
					}

					topicMapSystem = topicMapSystemFactory.NewTopicMapSystem();
				};
	}
}