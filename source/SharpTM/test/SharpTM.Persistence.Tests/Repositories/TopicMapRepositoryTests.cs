// <copyright file="RepositoryFactory.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Bridges;
using Xunit;
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

	public class When_loading_a_topic_map_with_null_id : With_Topic_Map_Repository
	{
		static object id;
		static Exception exception;

		Given an_illegal_id = () => id = null;
		Because of_loading = () => exception = Catch.Exception(() => repository.Import(id));
		It should_throw_an_exception = () => exception.ShouldBeType<ArgumentNullException>();
	}

	public class When_loading_a_topic_map_system_with_valid_id : With_Topic_Map_Repository
	{
		[Fact(Skip = "Not implemented yet.")]
		public new void Run() { }

		//static string id;
		//static TopicMapDTO tmDTO;

		//Given a_legal_id = () => id = "http://pixelplastic.de/topicmaps/public";
		//Because of_loading = () => tmDTO = repository.Import(id);
		//It should_load_the_topic_map_as_DTO = () => tmDTO.ShouldNotBeNull();
		//It should_load_the_correct_topic_map = () => tmDTO.ItemIdentities.Find(iid => iid.HRef == id).ShouldNotBeNull();
	}
}