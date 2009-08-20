// <copyright file="ReifiableToDTOTests.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.ToDTO;
using TMAPI.Net.Core;
using Xunit;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests.ToDTO
{
	public class When_mapping_a_reifiable_item : With_AssociatedAndNamedTopics
	{
		static IReifiable from;
		static VariantDTO to;
		static ReifiableToDTO mapper;

		Given a_reifiable = () => from = variant1;
		Given a_reifiable_DTO = () => to = new VariantDTO();
		Given a_mapper = () => mapper = new ReifiableToDTO();

		Because of_mapping_the_reifiable = () => mapper.Map(from, to);

		// It should_set_the_reifier_reference = () => to.Reifier.ShouldNotBeNull();
		// It should_refer_to_the_same_reifier = () => topicMap.GetConstructByItemIdentifier();
		[Fact(Skip = "Mappers are out of scope for now.")]
		public new void Run() { }
	}

	public class When_mapping_a_reifiable_item_without_reifier : With_AssociatedAndNamedTopics
	{
		static IReifiable from;
		static VariantDTO to;
		static ReifiableToDTO mapper;

		Given a_reifiable = () => from = variant2;
		Given a_reifiable_DTO = () => to = new VariantDTO();
		Given a_mapper = () => mapper = new ReifiableToDTO();

		Because of_mapping_the_reifiable = () => mapper.Map(from, to);

		It should_not_set_the_reifier = () => to.Reifier.ShouldBeNull();
	}
}