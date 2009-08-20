// <copyright file="VariantFromDTOTests.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO;
using TMAPI.Net.Core;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests
{
	public class When_mapping_a_variant_DTO_with_resource_locator : With_FilledTopicMapDTOandNames
	{
		static VariantDTO reverseVariant;
		static IVariant variant;
		static ITopicMap topicMap;
		static IName name;

		Given a_TMAPI_topic_map = () => topicMap = topicMapSystem.CreateTopicMap(TestHelper.CreateLocatorString("variantMapperTest"));
		Given a_TMAPI_name = () => name = topicMap.CreateTopic().CreateName(marcelHoyerName.Value);

		Given a_variant_DTO = () =>
		{
			reverseVariant = new VariantDTO();
			reverseVariant.Scope = new ScopeDTO();
			reverseVariant.Scope.TopicReferences.Add(TestHelper.CreateLocator("userName"));
			reverseVariant.ResourceReference = TestHelper.CreateLocator("variantResource");
			marcelHoyerName.Variants.Add(reverseVariant);
		};

		Because of_mapping_the_variant_DTO = () => variant = VariantFromDTO.Create(name, reverseVariant);

		It should_map_the_resource_reference = () => variant.Value.ShouldEqual(reverseVariant.ResourceReference.HRef);
		It should_map_the_resource_type = () => variant.Datatype.Reference.ShouldEqual("http://www.w3.org/2001/XMLSchema#anyURI");
	}

	public class When_mapping_a_variant_DTO_with_resource_data : With_FilledTopicMapDTOandNames
	{
		static VariantDTO reverseVariant;
		static IVariant variant;
		static ITopicMap topicMap;
		static IName name;

		Given a_TMAPI_topic_map = () => topicMap = topicMapSystem.CreateTopicMap(TestHelper.CreateLocatorString("variantMapperTest"));
		Given a_TMAPI_name = () => name = topicMap.CreateTopic().CreateName(marcelHoyerName.Value);

		Given a_variant_DTO = () =>
		{
			reverseVariant = new VariantDTO();
			reverseVariant.Scope = new ScopeDTO();
			reverseVariant.Scope.TopicReferences.Add(TestHelper.CreateLocator("userName"));
			reverseVariant.ResourceData = new ResourceDataDTO();
			reverseVariant.ResourceData.Text = "mhoyer";
			
			marcelHoyerName.Variants.Add(reverseVariant);
		};

		Because of_mapping_the_variant_DTO = () => variant = VariantFromDTO.Create(name, reverseVariant);

		It should_map_the_resource_data = () => variant.Value.ShouldEqual(reverseVariant.ResourceData.Text);
		It should_set_the_correct_resource_type = () => variant.Datatype.Reference.ShouldEqual("http://www.w3.org/2001/XMLSchema#string");
	}

	public class When_mapping_a_variant_DTO_without_any_scope : With_FilledTopicMapDTOandNames
	{
		static VariantDTO reverseVariant;
		static IVariant variant;
		static ITopicMap topicMap;
		static IName name;
		static Exception exception;

		Given a_TMAPI_topic_map = () => topicMap = topicMapSystem.CreateTopicMap(TestHelper.CreateLocatorString("variantMapperTest"));
		Given a_TMAPI_name = () => name = topicMap.CreateTopic().CreateName(marcelHoyerName.Value);

		Given a_variant_DTO = () =>
		{
			reverseVariant = new VariantDTO();
			reverseVariant.ResourceReference = TestHelper.CreateLocator("variantResource");
			marcelHoyerName.Variants.Add(reverseVariant);
		};

		Because of_mapping_the_variant_DTO = () => exception = Catch.Exception(() => variant = VariantFromDTO.Create(name, reverseVariant));

		It should_throw_a_not_supported_exception = () => exception.ShouldBeType<MappingException>();
	}

	public class When_mapping_a_variant_DTO_without_resource_information : With_FilledTopicMapDTOandNames
	{
		static VariantDTO reverseVariant;
		static IVariant variant;
		static ITopicMap topicMap;
		static IName name;
		static Exception exception;

		Given a_TMAPI_topic_map = () => topicMap = topicMapSystem.CreateTopicMap(TestHelper.CreateLocatorString("variantMapperTest"));
		Given a_TMAPI_name = () => name = topicMap.CreateTopic().CreateName(marcelHoyerName.Value);

		Given a_variant_DTO = () =>
		{
			reverseVariant = new VariantDTO();
			reverseVariant.Scope = new ScopeDTO();
			reverseVariant.Scope.TopicReferences.Add(TestHelper.CreateLocator("userName"));
			marcelHoyerName.Variants.Add(reverseVariant);
		};

		Because of_mapping_the_variant_DTO = () => exception = Catch.Exception(() => variant = VariantFromDTO.Create(name, reverseVariant));

		It should_throw_a_not_supported_exception = () => exception.ShouldBeType<MappingException>().InnerException.ShouldBeType<ArgumentException>();
	}
}