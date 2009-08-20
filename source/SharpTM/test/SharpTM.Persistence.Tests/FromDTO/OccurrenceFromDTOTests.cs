// <copyright file="OccurrenceFromDTOTests.cs" company="Pixelplastic">
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
	public class When_mapping_an_occurrence_DTO_with_resource_data : With_Filled_TopicMapDTO_and_occurrences
	{
		static ITopicMap topicMap;
		static IOccurrence occurrence;

		Given an_empty_TMAPI_topic_map =
			() => topicMap = topicMapSystem
			                 	.CreateTopicMap(TestHelper.CreateLocatorString(typeof(When_mapping_an_occurrence_DTO_with_resource_data).FullName));

		Because of_mapping_the_occurrence = () => occurrence = OccurrenceFromDTO.Create(topicMap.CreateTopic(), marcelHoyerAbstract);

		It should_map_the_type = () => occurrence.Type.ItemIdentifiers[0].Reference.ShouldEqual(marcelHoyerAbstract.Type.TopicReference.HRef);

		It should_set_the_datatype =
			() => occurrence.Datatype.Reference.ShouldEqual("http://www.w3.org/2001/XMLSchema#string");
		It should_set_the_value = () => occurrence.Value.ShouldEqual(marcelHoyerAbstract.ResourceData.Text);
	}

	public class When_mapping_an_occurrence_DTO_with_resource_data_and_explicit_data_type : With_Filled_TopicMapDTO_and_occurrences
	{
		static ITopicMap topicMap;
		static IOccurrence occurrence;

		Given the_explicit_data_type =
			() => marcelHoyerAbstract.ResourceData.Datatype = "http://www.w3.org/2001/XMLSchema#string";

		Given an_empty_TMAPI_topic_map =
			() => topicMap = topicMapSystem
			                 	.CreateTopicMap(TestHelper.CreateLocatorString(typeof(When_mapping_an_occurrence_DTO_with_resource_data_and_explicit_data_type).FullName));

		Because of_mapping_the_occurrence = () => occurrence = OccurrenceFromDTO.Create(topicMap.CreateTopic(), marcelHoyerAbstract);

		It should_set_the_internal_TMAPI_datatype_for_string =
			() => occurrence.Datatype.Reference.ShouldEqual("http://www.w3.org/2001/XMLSchema#string");
	}

	public class When_mapping_an_occurrence_DTO_with_resource_reference : With_Filled_TopicMapDTO_and_occurrences
	{
		static ITopicMap topicMap;
		static IOccurrence occurrence;

		Given an_empty_TMAPI_topic_map =
			() => topicMap = topicMapSystem
			                 	.CreateTopicMap(TestHelper.CreateLocatorString(typeof(When_mapping_an_occurrence_DTO_with_resource_reference).FullName));

		Because of_mapping_the_occurrence = () => occurrence = OccurrenceFromDTO.Create(topicMap.CreateTopic(), marcelHoyerImage);

		It should_map_the_type = () => occurrence.Type.ItemIdentifiers[0].Reference.ShouldEqual(marcelHoyerImage.Type.TopicReference.HRef);
		It should_set_the_datatype =
			() => occurrence.Datatype.Reference.ShouldEqual("http://www.w3.org/2001/XMLSchema#anyURI");
	}
}