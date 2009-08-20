// <copyright file="ConstructFromDTOTests.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO;
using TMAPI.Net.Core;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests.FromDTO
{
	public class When_mapping_a_construct_with_many_item_identifiers : With_TopicMapDTO
	{
		static ITopicMap topicMap;
		static ConstructDTO construct;

		Given a_construct_with_many_item_identifiers =
			() =>
				{
					topicMapDTO.ItemIdentities.Add(new LocatorDTO() { HRef = "http://sharptm.de/mhoyer" });
					topicMapDTO.ItemIdentities.Add(new LocatorDTO() { HRef = "http://marcelhoyer.de/me" });
					construct = topicMapDTO;
				};

		Because of_mapping_the_construct_DTO_to_a_TMAPI_construct
			= () => topicMap = TopicMapFromDTO.Create(topicMapSystem, (TopicMapDTO) construct);

		It should_contain_all_item_identifiers =
			() =>
				{
					foreach (LocatorDTO dto in topicMapDTO.ItemIdentities)
					{
						topicMap.ItemIdentifiers
							.ShouldContain(LocatorFromDTO.Create(topicMap, dto));
					}
				};

	}
}