// <copyright file="VariantToDTOTests.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.Core;
using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.Mapper.ToDTO;
using TMAPI.Net.Core;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests.ToDTO
{
	public class When_mapping_a_string_variant_to_DTO : With_AssociatedTopics
	{
		const string name = "Hoyer, Marcel";

		static IVariant from;
		static VariantDTO to;
		static VariantToDTO variantMapper;

		Given a_variant =
			() => from = topic1
			             	.CreateName("Marcel Hoyer")
			             	.CreateVariant(
			             	name,
			             	Datatypes.Locators.String,
			             	topicMap.CreateTopic());

		Given a_variant_mapper = () => variantMapper = new VariantToDTO();
		Given a_variantDTO = () => to = new VariantDTO();

		Because mapping_to_DTO = () => variantMapper.Map(from, to);

		It should_map_the_value = () => to.ResourceData.Text.ShouldContain(name);
		It should_initialize_the_scope = () => to.Scope.ShouldNotBeNull();
		It should_not_set_the_data_type = () => to.ResourceData.Datatype.ShouldBeNull();
		It should_not_set_a_resource_reference = () => to.ResourceReference.ShouldBeNull();
	}

	public class When_mapping_a_reference_variant_to_DTO : With_AssociatedTopics
	{
		const string uri = TOPIC_MAP_SID + "/mhoyer";

		static IVariant from;
		static VariantDTO to;
		static VariantToDTO variantMapper;

		Given a_variant =
			() => from = topic1
			             	.CreateName("Marcel Hoyer")
			             	.CreateVariant(
			             	uri,
			             	Datatypes.Locators.AnyUri,
			             	topicMap.CreateTopic());

		Given a_variant_mapper = () => variantMapper = new VariantToDTO();
		Given a_variantDTO = () => to = new VariantDTO();

		Because mapping_to_DTO = () => variantMapper.Map(from, to);

		It should_initialize_the_scope = () => to.Scope.ShouldNotBeNull();
		It should_not_set_resource_data = () => to.ResourceData.ShouldBeNull();
		It should_map_the_reference_uri = () => to.ResourceReference.HRef.ShouldContain(uri);
	}

	public class When_mapping_a_scoped_variant_to_DTO : With_AssociatedTopics
	{
		const string name = "Hoyer, Marcel";

		static IVariant from;
		static VariantDTO to;
		static VariantToDTO variantMapper;

		Given a_scoped_variant =
			() => from = topic1
			             	.CreateName("Marcel Hoyer")
			             	.CreateVariant(
			             	name,
			             	Datatypes.Locators.String,
			             	topicMap.CreateTopic(), topicMap.CreateTopic());

		Given a_variant_mapper = () => variantMapper = new VariantToDTO();
		Given a_variantDTO = () => to = new VariantDTO();

		Because mapping_to_DTO = () => variantMapper.Map(from, to);

		It should_initialize_the_scope = () => to.Scope.ShouldNotBeNull();
		It should_initialize_the_scope_topic_references = () => to.Scope.TopicReferences.ShouldNotBeNull();
		It should_map_the_scopes = 
			() =>
				{
					foreach (ITopic topic in from.Scope)
					{
						to.Scope
							.TopicReferences
							.FindAll(innerTopic =>
							         	{
							         		if (topic.ItemIdentifiers.Count > 0)
							         		{
							         			return innerTopic.HRef == topic.ItemIdentifiers[0].Reference;
							         		}
							         		else if (topic.SubjectIdentifiers.Count > 0)
							         		{
							         			return innerTopic.HRef == topic.SubjectIdentifiers[0].Reference;
							         		}

							         		return false;
							         	})
							.Count.ShouldEqual(1);
					}
				};
	}
}