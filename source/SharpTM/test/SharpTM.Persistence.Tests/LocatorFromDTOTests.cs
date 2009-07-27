// <copyright file="LocatorFromDTOTests.cs" company="Pixelplastic">
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
	public class When_mapping_a_locator : With_TopicMap
	{
		static LocatorDTO locatorDTO;
		static ILocator locator;

		Given a_locator_DTO = () => locatorDTO = new LocatorDTO() { HRef = TOPIC_MAP_SID };
		Because of_creating_an_TMAPI_instance = () => locator = LocatorFromDTO.Create(topicMapSystem, locatorDTO);
		It should_match_the_reference_value = () => locator.Reference.ShouldEqual(TOPIC_MAP_SID);
	}
}