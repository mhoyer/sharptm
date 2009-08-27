// <copyright file="RepositoryFactory.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Bridges;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Connectors;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Repositories;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests.Repositories
{
	public class When_trying_to_get_a_topic_map_repository_instance_with_null_connector : With_TopicMapSystem
	{
		static TopicMapBridge bridge;
		static Exception exception;

		Given the_topic_map_bridge = () => bridge = new TopicMapBridge(topicMapSystem);

		Because of_trying_to_create_the_instance =
			() => exception = Catch.Exception(() => new TopicMapRepository(null, bridge));

		It should_throw_an_exception = () => exception.ShouldBeType<ArgumentNullException>();
	}

	public class When_trying_to_get_a_topic_map_repository_instance_with_null_bridge : With_TopicMapSystem
	{
		static XTMConnector cnx;
		static Exception exception;

		Given a_connector = () => cnx = new XTMConnector();

		Because of_trying_to_create_the_instance =
			() => exception = Catch.Exception(() => new TopicMapRepository(cnx, null));

		It should_throw_an_exception = () => exception.ShouldBeType<ArgumentNullException>();
	}
}