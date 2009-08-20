// <copyright file="NameFromDTOTests.cs" company="Pixelplastic">
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
	public class When_mapping_a_name : With_Filled_TopicMapDTO_and_names
	{
		static ITopicMap topicMap;
		static ITopic topic;
		static IName name;
		static ILocator topicName;

		Given an_empty_topicMap = () => topicMap = topicMapSystem.CreateTopicMap(TestHelper.CreateLocatorString("foo")); 
		Given an_empty_topic = () => topic = topicMap.CreateTopic();
		Given the_TopicName_locator = () => topicName = topicMapSystem.CreateLocator("http://psi.topicmaps.org/iso13250/model/topic-name");

		Because of_mapping_the_name = () => name = NameFromDTO.Create(topic, marcelHoyerName);

		It should_create_the_TopicName_type = () => topicMap.GetTopicBySubjectIdentifier(topicName).ShouldNotBeNull();
		It should_map_the_type = () => name.Type.ShouldEqual(topicMap.GetTopicBySubjectIdentifier(topicName));
		It should_map_the_value = () => name.Value.ShouldEqual(marcelHoyerName.Value);
		It should_map_the_variants = () => name.Variants.Count.ShouldEqual(marcelHoyerName.Variants.Count);
	}
}