// <copyright file="TopicMapFromDTOTests.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO;
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

}