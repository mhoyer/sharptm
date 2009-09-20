// <copyright file="LocatorToDTOTests.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.Mapper.ToDTO;
using TMAPI.Net.Core;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;
using STM = Pixelplastic.TopicMaps.SharpTM.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests.ToDTO
{
	public class When_mapping_a_Locator_to_DTO : With_TopicMapSystem
	{
		const string LOCATOR_TEST = "http://www.pixelplastic.de/";
		static ILocator from;
		static LocatorToDTO mapper;
		static LocatorDTO to;

		Given an_initialized_locator_mapper = () => mapper = new LocatorToDTO();
		Given an_initialized_source = () => from = topicMapSystem.CreateLocator(LOCATOR_TEST);
		Given an_initialized_target = () => to = new LocatorDTO();

		Because of_mapping_from_source_to_target = () => mapper.Map(from, to);

		It should_set_the_HRef_attribute = () => to.HRef.ShouldEqual(LOCATOR_TEST);
	}

	public class When_mapping_a_Locator_to_DTO_with_a_source_that_is_null
		: With_TopicMapSystem
	{
		static Exception exception;
		static ILocator from;
		static LocatorToDTO mapper;
		static LocatorDTO to;

		Given an_initialized_locator_mapper = () => mapper = new LocatorToDTO();
		Given an_initialized_target = () => to = new LocatorDTO();
		Given an_null_source = () => from = null;

		Because of_mapping_from_source_to_target = () => exception = Catch.Exception(() => mapper.Map(from, to));

		It should_set_the_HRef_attribute = () => exception.ShouldBeType<ArgumentNullException>();
	}

	public class When_mapping_a_Locator_to_DTO_with_a_target_that_is_null
		: With_TopicMapSystem
	{
		const string LOCATOR_TEST = "http://www.pixelplastic.de/";
		static Exception exception;
		static ILocator from;
		static LocatorToDTO mapper;
		static LocatorDTO to;

		Given an_initialized_locator_mapper = () => mapper = new LocatorToDTO();
		Given an_initialized_source = () => from = topicMapSystem.CreateLocator(LOCATOR_TEST);
		Given an_null_target = () => to = null;

		Because of_mapping_from_source_to_target = () => exception = Catch.Exception(() => mapper.Map(from, to));

		It throws_an_ArgumentNullException = () => exception.ShouldBeType<ArgumentNullException>();
	}
}