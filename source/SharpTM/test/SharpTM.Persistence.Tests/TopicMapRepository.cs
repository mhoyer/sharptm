// <copyright file="RepositoryFactory.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.Persistence.Bridges;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Connectors;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Repositories;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests
{
	public class When_trying_to_get_an_topic_map_repository_instance : With_TopicMapSystem
	{
		static TopicMapRepository instance;
		static XTMConnector cnx;
		static TopicMapBridge bridge;

        Given a_connector = () => cnx = new XTMConnector();
		Given the_topic_map_bridge = () => bridge = new TopicMapBridge(topicMapSystem);

		Because of_trying_to_get_the_instance =
			() => instance = new TopicMapRepository(cnx, bridge);

		It should_return_an_instance = () => instance.ShouldNotBeNull();
	}
}