// <copyright file="ConstructToDTOTests.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.ToDTO;
using TMAPI.Net.Core;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests.ToDTO
{
	public class When_mapping_a_topic_construct : With_AssociatedTopics
	{
		static TopicToDTO mapper;
		static ITopic from;
		static TopicDTO to;

		Given a_source_construct_with_item_identifiers = () =>
		                                                 	{ 
		                                                 		from = topic1;
		                                                 		from.AddItemIdentifier(from.Parent.CreateLocator(TOPIC_MAP_SID + "/tid1234"));
		                                                 		from.AddItemIdentifier(from.Parent.CreateLocator(TOPIC_MAP_SID + "/tid3456"));
		                                                 	};

		Given a_target_construct_DTO = () => to = new TopicDTO();
		Given a_construct_mapper = () => mapper = new TopicToDTO();

		Because of_mapping_from_construct_to_DTO = () => mapper.Map(from, to);

		It should_add_two_items = () => to.ItemIdentities.Count.ShouldBeInRange(2, 3);
		It should_map_the_item_identifiers =
			() =>
				{
					foreach (ILocator locator in topic1.ItemIdentifiers)
					{
						to.ItemIdentities.FindAll(x => x.HRef == locator.Reference).ShouldNotBeEmpty();
					}
				};
	}
}