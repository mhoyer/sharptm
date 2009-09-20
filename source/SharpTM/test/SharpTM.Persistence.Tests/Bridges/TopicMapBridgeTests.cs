// <copyright file="TopicMapBridge.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.Bridges;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests.Bridges
{
	public class When_instanciating_a_topic_map_bridge_with_null_TopicMapSystem : BDDTest
	{
		static Exception exception;
		Because of_creatining_the_instance = () => exception = Catch.Exception(() => new TopicMapBridge(null));
		It should_throw_an_exception = () => exception.ShouldBeType<ArgumentNullException>();
	}

	public class When_instanciating_a_topic_map_bridge : With_TopicMapSystem
	{
		static TopicMapBridge topicMapBridge;
		Because of_creatining_the_instance = () => topicMapBridge = new TopicMapBridge(topicMapSystem);
		It should_handle_the_TopicMapSystem = () => topicMapBridge.TopicMapSystem.ShouldEqual(topicMapSystem);
	}
}