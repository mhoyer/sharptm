// <copyright file="TypeFromDTOTests.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO;
using TMAPI.Net.Core;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests.FromDTO
{
	public class When_mapping_a_type_DTO : With_Filled_TopicMapDTO
	{
		static ITopic topicType;
		static ITopicMap topicMap;

		Given a_sample_topic_map =
			() => topicMap = topicMapSystem.CreateTopicMap("http://sharptm.de/TypeFromDTOTests");
		Because of_mapping = () => topicType = TypeFromDTO.FindOrCreate(topicMap, projectType);

		It should_create_one_and_only_one_TMAPI_topic = () => topicMap.Topics.Count.ShouldEqual(1);
		It should_map_the_type_DTO_to_a_topic = 
			() =>
				{
					topicMap.Topics[0].ItemIdentifiers.ShouldContain(LocatorFromDTO.Create(topicMap, projectType.TopicReference));
				};
	}

	public class When_mapping_a_type_DTO_while_TMAPI_topic_already_exists_with_item_identifier : With_Filled_TopicMapDTO
	{
		static ITopic topicType;
		static ITopicMap topicMap;
		static ITopic existingTopic;

		Given a_sample_topic_map =
			() => topicMap = topicMapSystem.CreateTopicMap("http://sharptm.de/TypeFromDTOTests");

		Given the_existing_topic =
			() => existingTopic = topicMap.CreateTopicByItemIdentifier(LocatorFromDTO.Create(topicMapSystem, projectType.TopicReference));

		Because of_mapping = () => topicType = TypeFromDTO.FindOrCreate(topicMap, projectType);

		It should_find_or_merge_the_type_DTO = () => topicType.ShouldEqual(existingTopic);
	}

	public class When_mapping_a_type_DTO_while_TMAPI_topic_already_exists_with_subject_identifier : With_Filled_TopicMapDTO
	{
		static ITopic topicType;
		static ITopicMap topicMap;
		static ITopic existingTopic;

		Given a_sample_topic_map =
			() => topicMap = topicMapSystem.CreateTopicMap("http://sharptm.de/TypeFromDTOTests");

		Given the_existing_topic =
			() => existingTopic = topicMap.CreateTopicBySubjectIdentifier(LocatorFromDTO.Create(topicMapSystem, projectType.TopicReference));

		Because of_mapping = () => topicType = TypeFromDTO.FindOrCreate(topicMap, projectType);

		It should_find_or_merge_the_type_DTO = () => topicType.ShouldEqual(existingTopic);
	}

	public class When_mapping_a_type_DTO_while_TMAPI_topic_already_exists_with_subject_locator : With_Filled_TopicMapDTO
	{
		static ITopic topicType;
		static ITopicMap topicMap;
		static ITopic existingTopic;

		Given a_sample_topic_map =
			() => topicMap = topicMapSystem.CreateTopicMap("http://sharptm.de/TypeFromDTOTests");

		Given the_existing_topic =
			() => existingTopic = topicMap.CreateTopicBySubjectLocator(LocatorFromDTO.Create(topicMapSystem, projectType.TopicReference));

		Because of_mapping = () => topicType = TypeFromDTO.FindOrCreate(topicMap, projectType);

		It should_find_or_merge_the_type_DTO = () => topicType.ShouldEqual(existingTopic);
	}
}