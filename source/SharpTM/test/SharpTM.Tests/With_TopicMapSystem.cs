// <copyright file="With_TopicMapSystem.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Microsoft.Practices.ServiceLocation;
using Pixelplastic.TopicMaps.SharpTM.Core;
using Pixelplastic.TopicMaps.SharpTM.Helper;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Repositories.InMemory;
using TMAPI.Net.Core;
using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Tests
{
	public abstract class With_TopicMapSystem : BDDTest
	{
		protected static ITopicMapSystem topicMapSystem;

		Given the_service_locator =
			() =>
				{
					SimpleServiceLocator ssl = new SimpleServiceLocator();
					ssl.Register<ITopicMapRepository>(new TopicMapRepository());
					ServiceLocator.SetLocatorProvider(() => ssl);
				};

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