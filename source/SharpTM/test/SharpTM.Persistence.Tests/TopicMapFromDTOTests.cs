// <copyright file="TopicMapFromDTOTests.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO;
using TMAPI.Net.Core;
using Xunit;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests
{
	public class When_mapping_from_topic_map_DTO_without_identifiers : With_TopicMapDTO
	{
		static Exception exception; 

		Given a_topic_map_DTO_without_ids = () => topicMapDTO.ItemIdentities.Clear();
		Because of_mapping = () => exception = Catch.Exception(() => TopicMapFromDTO.Create(topicMapSystem, topicMapDTO));
		It should_throw_an_exception = () => exception.ShouldBeType<MappingException>();
	}

	public class When_mapping_from_topic_map_DTO : With_TopicMapDTO
	{
		Because of_mapping_the_topic_map = 
			() => TopicMapFromDTO.Create(topicMapSystem, topicMapDTO);

		It should_create_and_add_a_new_TMAPI_topic_map_to_the_system = 
			() => topicMapSystem.GetTopicMap(TOPIC_MAP_SID).ShouldNotBeNull();
	}

	public class When_mapping_from_topic_map_DTO_with_topics : With_FilledTopicMapDTO
	{
		static ITopicMap topicMap;

		Because of_mapping_the_topic_map =
			() => topicMap = TopicMapFromDTO.Create(topicMapSystem, topicMapDTO);

		It should_create_and_add_new_TMAPI_topics =
			() => topicMap.Topics.Count.ShouldEqual(topicMapDTO.Topics.Count);

		It should_map_the_SharpTM_topic =
			() => topicMap.GetTopicBySubjectIdentifier(topicMap.CreateLocator(sharpTM.SubjectIdentifiers[0].HRef));
	}

	public class When_mapping_from_topic_map_DTO_with_associations : With_FilledTopicMapDTO
	{
		static ITopicMap topicMap;

		Because of_mapping_the_topic_map =
			() => topicMap = TopicMapFromDTO.Create(topicMapSystem, topicMapDTO);

		It should_create_and_add_new_TMAPI_associations =
			() => topicMap.Associations.Count.ShouldEqual(topicMapDTO.Associations.Count);
	}

	public class When_mapping_from_topic_map_DTO_with_merge_maps : With_FilledTopicMapDTO
	{
		Because of_mapping_the_topic_map =
			() => TopicMapFromDTO.Create(topicMapSystem, topicMapDTO);

		// TODO: It should_handle_the_merge_maps = () => 
		[Fact(Skip = "Not implemented.")]
		public new void Run()
		{
		}
	}

	public class When_mapping_from_topic_map_DTO_with_reifier : With_FilledTopicMapDTO
	{
		Because of_mapping_the_topic_map =
			() => TopicMapFromDTO.Create(topicMapSystem, topicMapDTO);

		// TODO: It should_handle_the_merge_maps = () => 
		[Fact(Skip = "Not implemented.")]
		public new void Run()
		{
		}
	}

}
