// <copyright file="TopicFromDTOTests.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO;
using TMAPI.Net.Core;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests
{
	public class When_mapping_a_topic_with_many_identifiers : With_TopicDTO
	{
		static ITopic topic;

		Because of_mapping_the_topic_DTO_to_a_TMAPI_topic = () => topic = TopicFromDTO.Create(topicMap, identifiedTopicDTO);

		It should_contain_all_item_identifiers =
			() =>
				{
					foreach (LocatorDTO dto in identifiedTopicDTO.ItemIdentities)
					{
						topic.ItemIdentifiers
							.ShouldContain(LocatorFromDTO.Create(topicMap, dto));
					}
				};

		It should_contain_all_subject_identifiers =
			() =>
				{
					foreach (LocatorDTO dto in identifiedTopicDTO.SubjectIdentifiers)
					{
						topic.SubjectIdentifiers
							.ShouldContain(LocatorFromDTO.Create(topicMap, dto));
					}
				};

		It should_contain_all_subject_locators =
			() =>
				{
					foreach (LocatorDTO dto in identifiedTopicDTO.SubjectLocators)
					{
						topic.SubjectLocators
							.ShouldContain(LocatorFromDTO.Create(topicMap, dto));
					}
				};
	}
}